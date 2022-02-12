import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export const SMALL_WIDTH_BREAKPOINT = 1100;

@Injectable({
  providedIn: 'root',
})
export class LayoutService {
  //
  private readonly LIGHT_THEME = 'light-theme';

  private readonly _topbarTitle = new BehaviorSubject('');
  readonly topbarTitle$ = this._topbarTitle.asObservable();

  private readonly _isLightTheme = new BehaviorSubject(false);
  readonly isLightTheme$ = this._isLightTheme.asObservable();

  private readonly _isScreenSmall = new BehaviorSubject(false);
  readonly isScreenSmall$ = this._isScreenSmall.asObservable();

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private breakpointObserver: BreakpointObserver
  ) {
    this.subscribeToSizeObservable();
  }

  setTopbarTitle(title: string) {
    this._topbarTitle.next(title);
  }

  toggleTheme() {
    this.setTheme(!this._isLightTheme.value);
  }

  setTheme(value: boolean) {
    value ? this.selectLightTheme() : this.selectDarkTheme();

    this._isLightTheme.next(value);
  }

  private selectDarkTheme(): void {
    this.document?.documentElement.classList.remove(this.LIGHT_THEME);
  }

  private selectLightTheme(): void {
    this.document?.documentElement.classList.add(this.LIGHT_THEME);
  }

  private subscribeToSizeObservable() {
    this.breakpointObserver
      .observe([`(max-width: ${SMALL_WIDTH_BREAKPOINT}px)`])
      // .observe( [Breakpoints.Medium] )
      .subscribe((state: BreakpointState) => {
        this._isScreenSmall.next(state.matches);
      });
  }
}
