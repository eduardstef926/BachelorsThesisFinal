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
  phoneNumber = '+40 0743886803';
  selectedCity!: string;
  currentIndex = 1;
  pageSize = 3;
  elementsFiltered = false;
  pageIndexes: number[] = [];
  dictionaryCopy!: Map<string, Array<HospitalDto>>;
  locationDictionary!: Map<string, Array<HospitalDto>>;

  constructor(private hospitalService: HospitalService) {}

  ngOnInit(): void {
    this.dictionaryCopy = new Map<string, Array<HospitalDto>>();
    this.locationDictionary = new Map<string, Array<HospitalDto>>();
    this.hospitalService.getAllHospitals().subscribe((hospitals: Array<HospitalDto>) => {
      this.renderPaginator(hospitals);
      this.mapLocationDictionary(hospitals);
    });
  }

  renderPaginator(hospitals: Array<HospitalDto>) {
    const numberLength = hospitals.length % this.pageSize == 0 ? hospitals.length / this.pageSize : hospitals.length / this.pageSize + 1;
    for(let i=1; i<=numberLength; i++) {
      this.pageIndexes.push(i);
    }
  }

  mapLocationDictionary(hospitals: Array<HospitalDto>) {
    hospitals.forEach((hospital: HospitalDto) => {
      if (this.locationDictionary.has(hospital.location)) {
        var hospitals = this.locationDictionary.get(hospital.location) as HospitalDto[];
        hospitals.push(hospital);
        this.locationDictionary.set(hospital.location, hospitals);
      } else {
        this.locationDictionary.set(hospital.location, [hospital]);
      }
    });
    this.dictionaryCopy = this.locationDictionary;
    this.locationDictionary = this.sliceMap(this.dictionaryCopy, 3*(this.currentIndex-1), 3*this.currentIndex - 1);
  }

  filterLocations() {
    var filteredList =  new Map<string, Array<HospitalDto>>();
    this.dictionaryCopy.forEach((value, key) => {
      if (key == this.selectedCity) {
        filteredList.set(key, value);
      }
    });
    this.elementsFiltered = true;
    this.locationDictionary = filteredList;
  }

  callNumber() {
    window.location.href = 'whatsapp://send?phone=' + this.phoneNumber;
  }

  sliceMap<K, V>(map: Map<K, V>, startIndex: number, endIndex: number) {
    const slicedMap = new Map<K, V>();
    const entries = Array.from(map.entries()).slice(startIndex, endIndex + 1);
    for (const [key, value] of entries) {
      slicedMap.set(key, value);
    }
    return slicedMap;
  }

  choosePage(index: number) {  
    this.locationDictionary = this.sliceMap(this.dictionaryCopy, 3*(index-1), 3*index-1);
    this.currentIndex = index;
  }

  movePage(direction: string, index: number) {
    switch (direction) {
      case "fixedPosition":
          this.locationDictionary = this.sliceMap(this.dictionaryCopy, 3*(index-1), 3*index-1);
          this.currentIndex = index;
          break;
        case "forwards":
          this.currentIndex += 1;
          this.locationDictionary = this.sliceMap(this.dictionaryCopy, 3*(this.currentIndex-1), 3*this.currentIndex-1);
          break;
        case "backwards":
          this.currentIndex -= 1;
          this.locationDictionary = this.sliceMap(this.dictionaryCopy, 3*(this.currentIndex-1), 3*this.currentIndex-1);
          break;
    }

  }
}
