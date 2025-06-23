export class SiteSetting {
    id : number = 0;
    siteName : string = '';
    siteLogo : string = '';
    siteTitle : string = '';
    footerTitle : string = '';
    defaultPassword : string = '';
    siteLogoFile: File | null = null;
    remark : string = '';
    isActive : boolean = true;
}
