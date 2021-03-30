import { Injectable } from '@angular/core';
import { Property } from './property.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Address } from './address.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PropertyService {

propertyListDb: Property[];
propertyListJson: Property[];
readonly rootUrl: string = "http://localhost:55507/api"
readonly propertyJsonUrl: string = "https://samplerspubcontent.blob.core.windows.net/public/properties.json"

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  getSavedProperties(){
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, DELETE, PUT',
        'Access-Control-Allow-Origin': '*'
        })
      };

    return this.http.get(this.rootUrl+"/properties", httpOptions).subscribe(response => {
      this.propertyListDb = <Property[]>response;
    });
  }

  getProperties(){
    return this.http.get(this.propertyJsonUrl).subscribe(response => {
      this.propertyListJson = [];

      response["properties"].forEach(element => {
        let property: Property = new Property();

        let propertyTemp: Property;
        if(this.propertyListDb != null)
          propertyTemp = this.propertyListDb.filter(temp=>temp["jsonId"]==element.id)[0];

        if (propertyTemp != null && propertyTemp != undefined)
          property.Id = propertyTemp["id"];
        else
          property.Id = 0;
        property.JsonId = element.id;
        property.YearBuilt = (element.physical != null) ? element.physical.yearBuilt: 0;
        property.ListPrice = (element.financial != null) ? element.financial.listPrice: 0;
        property.MonthlyRent = (element.financial != null) ? element.financial.monthlyRent: 0;
        property.GrossYield = (element.financial != null) ? (element.financial.monthlyRent*12)/element.financial.listPrice : 0;

        let address: Address = new Address()
		    address.Address1 = element.address.address1;
		    address.Address2 = element.address.address2;
        address.City = element.address.city;
        address.Country = element.address.country;
        address.County = element.address.county;
        address.District = element.address.district;
        address.State = element.address.state;
        address.Zip = element.address.zip;
        address.ZipPlus4 = element.address.zipPlus4;

        property.Address = address;

        this.propertyListJson.push(property);
      });
    });
  }

  savedProperty(property: Property  ){
    return this.http.post(this.rootUrl+"/properties", property);
  }
}
