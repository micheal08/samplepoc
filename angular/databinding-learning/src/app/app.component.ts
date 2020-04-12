import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'databinding-learning';
  serverElements: [{type: string, name: string, content: string}] =
    [{type: 'server', name: 'Test Server', content: 'Test Content'}];


    // LifeCycleHooks
    // 1. ngOnChange - Called after a bound input property changes
    // 2. ngOnInit - Called once the component is initialized
    // 3. ngDoCheck - Called during every change detection run
    // 4. ngAferContentInit -  Called after content(ng-content) has been projected into view
    // 5. ngAfterContentChecked -  Called every time projected content has been checked
    // 6. ngAfterViewInit -  Called after the component's view (and child views) has been initialized
    // 7. ngAfterViewChecked -  Called every time the view (and child views) have been checked
    // 8. ngOnDestroy -  Called once the component is about to be destroyed

  constructor() {
  }

  onServerCreated(serverData: {serverName: string, serverContent: string}) {
    this.serverElements.push({
      type: 'server',
      name: serverData.serverName,
      content: serverData.serverContent
    });
  }

  onBlurPrintCreated(serverData: {serverName: string, serverContent: string}) {
    this.serverElements.push({
      type: 'blueprint',
      name: serverData.serverName,
      content: serverData.serverContent
    });
  }
}
