import{aI as t,aJ as n}from"./index.e82e929c.js";class o{constructor(e){this.id=e?e.id:0,this.name=e?e.name:null,this.balance=e?e.balance:0,this.concurrencyStamp=e?e.concurrencyStamp:null,this.email=e?e.secret_key:null,this.hasPassword=e?e.hasPassword:!1,this.isExternal=e?e.isExternal:!1,this.phoneNumber=e?e.phoneNumber:null,this.surname=e?e.surname:null,this.userName=e?e.userName:null}}class c{constructor(e){this.id=e?e.id:null,this.base64Image=e?e.base64Image:null,this.expiredTime=e?e.expiredTime:null}}class u{constructor(e){this.id=e?e.id:0,this.userId=e?e.userId:0,this.fullname=e?e.fullname:"",this.phoneNumber=e?e.phoneNumber:"",this.agencyKey=e?e.agencyKey:"",this.bankName=e?e.bankName:"",this.accountName=e?e.accountName:"",this.bankNumber=e?e.bankNumber:"",this.title=e?e.title:"",this.address=e?e.address:"",this.qrCode=e?new c(e.qrCode):new c,this.contact=e?e.contact:"",this.domain=e?e.domain:"",this.status=e?e.status:""}}const l="api/app/profile",h="api/app/profile/generate-qrcode",b="api/app/agency/user-agency",g="api/app/agency/register-agency",p="api/app/agency/register-agency",d="api/app/profile/change-password",y=t({id:"user",state:()=>({user:JSON.parse(localStorage.getItem("user")),agency:JSON.parse(localStorage.getItem("agency"))}),getters:{getUser:s=>s.user},actions:{fetchProfile(s={}){return new Promise((e,a)=>{n.get(`/${l}`,s).then(({data:r})=>{localStorage.user=JSON.stringify(new o(r)),e(r)}).catch(({response:r})=>{a(r)})})},fetchAgency(s={}){return new Promise((e,a)=>{n.get(`/${b}`,s).then(({data:r})=>{localStorage.agency=JSON.stringify(new u(r)),e(r)}).catch(({response:r})=>{a(r)})})},registerAgency(s={}){return new Promise((e,a)=>{n.post(`/${g}`,s).then(({data:r})=>{e(r)}).catch(({response:r})=>{a(r)})})},updateAgency(s={}){return new Promise((e,a)=>{n.post(`/${p}`,s).then(({data:r})=>{e(r)}).catch(({response:r})=>{a(r)})})},changePassword(s={}){return new Promise((e,a)=>{n.post(`/${d}`,s).then(({data:r})=>{e(r)}).catch(({response:r})=>{a(r)})})},generateCode(s={}){return new Promise((e,a)=>{n.post(`/${h}`,s).then(({data:r})=>{e(r)}).catch(({response:r})=>{a(r)})})}}}),N=t({id:"breadcrumb",state:()=>({breadcrumbs:[]}),getters:{pageBreadcrumbs:s=>s.breadcrumbs,pageTitle:s=>{let e=s.breadcrumbs[1]||s.breadcrumbs[s.breadcrumbs.length-1];if(e&&e.title)return e.title},pageIcon:s=>{let e=s.breadcrumbs[1]||s.breadcrumbs[s.breadcrumbs.length-1];if(e&&e.icon)return e.icon}},actions:{SET_BREADCRUMB(s){this.breadcrumbs=s},APPEND_BREADCRUMB(s){this.breadcrumbs=[...this.breadcrumbs,s]}}});export{u as A,o as U,N as b,y as u};