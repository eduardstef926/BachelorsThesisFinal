import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { HospitalService } from '../services/hospital.service';
import { HospitalDto } from '../model/hospital.model';

@Component({
  selector: 'app-locations-page',
  templateUrl: './locations-page.component.html',
  styleUrls: ['./locations-page.component.scss'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ opacity: 1 }),
        animate(1000, style({ opacity: 0 }))
      ])
    ])
  ]
})
export class LocationPageComponent implements OnInit {
  phoneNumber = '+400774487082';
  selectedCity!: string;
  dictionaryCopy!: Map<string, Array<HospitalDto>>;
  locationDictionary!: Map<string, Array<HospitalDto>>;

  constructor(private hospitalService: HospitalService) {}

  ngOnInit(): void {
    this.locationDictionary = new Map<string, Array<HospitalDto>>();
    this.hospitalService.getAllHospitals().subscribe((hospitals: Array<HospitalDto>) => {
      hospitals.forEach((hospital: HospitalDto) => {
        this.mapLocationDictionary(hospital);
      });
      this.dictionaryCopy = this.locationDictionary;
    });
  }

  mapLocationDictionary(hospital: HospitalDto) {
    if (this.locationDictionary.has(hospital.location)) {
      var hospitals = this.locationDictionary.get(hospital.location) as HospitalDto[];
      hospitals.push(hospital);
      this.locationDictionary.set(hospital.location, hospitals);
    } else {
      this.locationDictionary.set(hospital.location, [hospital]);
    }
  }

  filterLocations() {
    var filteredList =  new Map<string, Array<HospitalDto>>();
    this.dictionaryCopy.forEach((value, key) => {
      if (key == this.selectedCity) {
        filteredList.set(key, value);
      }
    });
    this.locationDictionary = filteredList;
  }

  callNumber() {
    window.location.href = 'whatsapp://send?phone=' + this.phoneNumber;
  }
}
