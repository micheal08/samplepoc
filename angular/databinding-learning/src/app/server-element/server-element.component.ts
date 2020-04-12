import { Component, OnInit, Input, ContentChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-server-element',
  templateUrl: './server-element.component.html',
  styleUrls: ['./server-element.component.css']
})

export class ServerElementComponent implements OnInit {
  @Input() element: {type: string, name: string, content: string};
  @ContentChild('contentParagraph') contentPara: ElementRef;

  constructor() { }

  ngOnInit(): void {
  }

}
