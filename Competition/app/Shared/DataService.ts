import { Component, Injectable, Input, Output, EventEmitter } from '@angular/core'

@Injectable()
export class DataService {
    sharingData: number;
    saveData(str: number) {
        this.sharingData = str;
    }
    getData()
    {
        return this.sharingData;
    }
} 