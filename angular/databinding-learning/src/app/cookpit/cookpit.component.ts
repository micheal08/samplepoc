import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-cookpit',
  templateUrl: './cookpit.component.html',
  styleUrls: ['./cookpit.component.css']
})
export class CookpitComponent implements OnInit {
@Output() serverCreated = new EventEmitter<{serverName: string, serverContent: string}>();
@Output() bluePrintCreated = new EventEmitter<{serverName: string, serverContent: string}>();
newServerName = '';
newServerContent = '';
  constructor() { }

  ngOnInit(): void {
  }

  onServerAdded() {
    this.serverCreated.emit({
      serverName: this.newServerName,
      serverContent: this.newServerContent
    });
  }

  onBluePrintClicked() {
    this.bluePrintCreated.emit({
      serverName: this.newServerName,
      serverContent: this.newServerContent
    });
  }
}
