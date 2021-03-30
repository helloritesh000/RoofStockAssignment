import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PropertyService } from '../shared/property.service';

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent implements OnInit {

  constructor(private service: PropertyService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.getSavedProperties();
    this.service.getProperties();
    console.log("ngOnInit");
    // this.toastr.success('Hello world!', 'Toastr fun!');
  }

  saveProperty(property){
    this.service.savedProperty(property)
    .subscribe(data => {
      this.toastr.success('Saved record to Database', 'Status Message');
      window.location.reload();
    }),
    err => {
      this.toastr.error('Unable to save record to Database', 'Status Message');
      console.log("Error");
    };
  }

}
