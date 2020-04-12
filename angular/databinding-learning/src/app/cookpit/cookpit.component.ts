import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-cookpit',
  templateUrl: './cookpit.component.html',
  styleUrls: ['./cookpit.component.css']
})
export class CookpitComponent implements OnInit {
@Output() serverCreated = new EventEmitter<{serverName: string, serverContent: string}>();
@Output() bluePrintCreated = new EventEmitter<{serverName: string, serverContent: string}>();
// newServerName = '';
// newServerContent = '';

@ViewChild('serverContentInput') serverContentInput: ElementRef;

  constructor() { }

  ngOnInit(): void {
  }

  onServerAdded(nameinput: HTMLInputElement) {
    console.log(this.serverContentInput);
    this.serverCreated.emit({
      serverName: nameinput.value,
      serverContent: this.serverContentInput.nativeElement.value
    });
    // this.serverCreated.emit({
    //   serverName: nameinput.value,
    //   serverContent: this.newServerContent
    // });
  }

  onBluePrintClicked(nameinput: HTMLInputElement) {
    this.bluePrintCreated.emit({
      serverName: nameinput.value,
      serverContent: this.serverContentInput.nativeElement.value
    });
    // this.bluePrintCreated.emit({
    //   serverName: nameinput.value,
    //   serverContent: this.newServerContent
    // });
  }
}
