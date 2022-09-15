import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu-section',
  templateUrl: './menu-section.component.html',
  styleUrls: ['./menu-section.component.scss']
})
export class MenuSectionComponent implements OnInit {
  @Input() sectionName: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
