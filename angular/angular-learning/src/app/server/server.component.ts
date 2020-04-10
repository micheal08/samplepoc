import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})
export class ServerComponent implements OnInit {
  serverid = 10;
  serverstatus = 'offline';
  constructor() { }

  ngOnInit(): void {
    this.serverstatus = Math.random() > 0.5 ? 'online' : 'offline';
  }

  getServerStatus() {
    return this.serverstatus;
  }

  getColor() {
    return this.serverstatus === 'online' ? 'green' : 'red';
  }

}
