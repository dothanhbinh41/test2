import{b as k,u as _,A as h,U as V}from"./breadcrumb.c4def022.js";import{aK as P,b8 as $,b9 as S,aL as p,aM as m,aN as u,aO as s,aV as c,aU as g,aP as U,aS as a,aR as d,aW as b,aZ as f}from"./index.e82e929c.js";const N={name:"Profile",components:{Form:$,Field:S},setup(){const e=k(),l=_();return{useBreadcrumbStore:e,useUserStore:l}},data(){return{user:new h,agency:new h,loading:!0,issetAgency:!1,dialogVisible:!1,changePassword:{currentPassword:"",newPassword:"",confirmPassword:""}}},methods:{onSubmit(e){this.issetAgency?this.updateAgency(e):this.registerAgency(e)},onSubmitChange(e){this.onChangePassword(e)},getProfile(){this.useUserStore.fetchProfile().then(e=>{this.useUserStore.user=new V(e),this.user=this.useUserStore.user}).catch(e=>{})},getAgency(){this.useUserStore.fetchAgency().then(e=>{this.useUserStore.agency=new h(e),this.agency=this.useUserStore.agency,this.issetAgency=!0}).catch(e=>{})},registerAgency(e){let l=this.$t("agency")+" "+this.$t("has been register success");this.useUserStore.registerAgency(e).then(r=>{this.$notify.success({title:"Success",message:l})}).catch(r=>{this.$notify.error({title:"Error",message:r.data.error.message})})},updateAgency(e){let l=this.$t("agency")+" "+this.$t("has been change success");this.useUserStore.updateAgency(e).then(r=>{this.$notify.success({title:"Success",message:l})}).catch(r=>{this.$notify.error({title:"Error",message:r.data.error.message})})},onChangePassword(e){let l=this.$t("password")+" "+this.$t("has been change success");this.useUserStore.changePassword(e).then(r=>{this.resetForm,this.$notify.success({title:"Success",message:l})}).catch(r=>{this.$notify.error({title:"Error",message:r.data.error.message})})},resetForm(e){}},mounted(){this.getProfile(),this.getAgency(),this.useBreadcrumbStore.SET_BREADCRUMB([{title:"profile",icon:"far fa-uer"}])}},A={class:"row"},q={class:"col-lg-12"},C={class:"card mb-g"},F={class:"card-body pb-0 px-4"},B={class:"pb-3 pt-2 border-top-0 border-left-0 border-right-0 text-muted"},E={class:"form-row"},R={class:"col-md-4 mb-3"},j={class:"form-label"},D={key:0,class:"col-md-4 mb-3"},I={class:"form-label"},K=["placeholder","value"],M={key:1,class:"col-md-4 mb-3"},T={class:"form-label"},z=["placeholder","value"],L={class:"col-md-4 mb-3"},O={class:"form-label"},W={class:"col-md-4 mb-3"},Z={class:"form-label"},G={class:"col-md-4 mb-3"},H={class:"form-label"},J={class:"col-md-4 mb-3"},Q={class:"form-label"},X={class:"col-md-4 mb-3"},Y={class:"form-label"},x={class:"col-md-4 mb-3"},ee={class:"form-label"},se={class:"col-md-4 mb-3"},le={class:"form-label"},oe={class:"col-md-12 mb-3"},ae={class:"form-label"},te={class:"col-md-12 mb-3"},ne={class:"form-label"},de=["src"],ce={class:"flex flex-row justify-between"},ie={key:0,class:"el-icon-loading"},re={key:0,type:"submit",class:"btn bg-blue-600 text-white mr-2"},me={key:0,class:"el-icon-loading"},ue={key:1,type:"submit",class:"btn bg-blue-600 text-white"},be={key:0,class:"el-icon-loading"},he={class:"form-row p-3"},pe={class:"col-md-12 mb-3"},ge={class:"form-label"},fe={class:"col-md-12 mb-3"},ye={class:"form-label"},we={class:"col-md-12"},ve={class:"form-label"},ke={class:"dialog-footer justify-content-end flex pr-3 pt-3"},_e={type:"submit",class:"btn bg-blue-600 text-white"};function Ve(e,l,r,Pe,o,y){const i=p("Field"),w=p("Form"),v=p("el-dialog");return m(),u(U,null,[s("div",A,[s("div",q,[s("div",C,[s("div",F,[s("div",B,[c(w,{onSubmit:y.onSubmit},{default:g(({errors:t})=>[s("div",E,[s("div",R,[s("label",j,a(e.$t("name")),1),c(i,{value:o.agency.fullname,as:"input",rules:"required",class:d(["form-control",{"is-invalid":t.name}]),name:"fullname",placeholder:e.$t("placeholder")+e.$t("name")},null,8,["value","class","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.fullname}])},a(t.fullname),3)]),o.issetAgency?(m(),u("div",D,[s("label",I,a(e.$t("token")),1),s("input",{disabled:"",type:"text",class:"form-control",id:"token",placeholder:e.$t("placeholder")+e.$t("token"),autocomplete:"false",value:o.agency.userId},null,8,K)])):b("",!0),o.issetAgency?(m(),u("div",M,[s("label",T,a(e.$t("secret_key")),1),s("input",{disabled:"",type:"text",class:"form-control",id:"secret_key",placeholder:e.$t("placeholder")+e.$t("secret_key"),autocomplete:"false",value:o.agency.agencyKey},null,8,z)])):b("",!0),s("div",L,[s("label",O,a(e.$t("phone")),1),c(i,{modelValue:o.user.phoneNumber,"onUpdate:modelValue":l[0]||(l[0]=n=>o.user.phoneNumber=n),as:"input",name:"phoneNumber",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("phone"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.phoneNumber}])},a(t.phoneNumber),3)]),s("div",W,[s("label",Z,a(e.$t("bank_name")),1),c(i,{modelValue:o.agency.bankName,"onUpdate:modelValue":l[1]||(l[1]=n=>o.agency.bankName=n),as:"input",name:"bankName",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("bank_name"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.bankName}])},a(t.bankName),3)]),s("div",G,[s("label",H,a(e.$t("account_name")),1),c(i,{modelValue:o.agency.accountName,"onUpdate:modelValue":l[2]||(l[2]=n=>o.agency.accountName=n),as:"input",name:"accountName",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("account_name"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.accountName}])},a(t.accountName),3)]),s("div",J,[s("label",Q,a(e.$t("account_number")),1),c(i,{modelValue:o.agency.bankNumber,"onUpdate:modelValue":l[3]||(l[3]=n=>o.agency.bankNumber=n),as:"input",name:"bankNumber",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("account_name"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.bankNumber}])},a(t.bankNumber),3)]),s("div",X,[s("label",Y,a(e.$t("title")),1),c(i,{modelValue:o.agency.title,"onUpdate:modelValue":l[4]||(l[4]=n=>o.agency.title=n),as:"input",name:"title",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("title"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.title}])},a(t.title),3)]),s("div",x,[s("label",ee,a(e.$t("address")),1),c(i,{modelValue:o.agency.address,"onUpdate:modelValue":l[5]||(l[5]=n=>o.agency.address=n),as:"input",name:"address",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("address"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.address}])},a(t.address),3)]),s("div",se,[s("label",le,a(e.$t("domain")),1),c(i,{modelValue:o.agency.domain,"onUpdate:modelValue":l[6]||(l[6]=n=>o.agency.domain=n),as:"input",name:"domain",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("domain"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.domain}])},a(t.domain),3)]),s("div",oe,[s("label",ae,a(e.$t("contact")),1),c(i,{modelValue:o.agency.contact,"onUpdate:modelValue":l[7]||(l[7]=n=>o.agency.contact=n),as:"textarea",name:"contact",rules:"required",class:"form-control",placeholder:e.$t("placeholder")+e.$t("contact"),autocomplete:"false"},null,8,["modelValue","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.contact}])},a(t.contact),3)]),s("div",te,[s("label",ne,a(e.$t("qrcode")),1),s("img",{width:"300",src:`data:image/png;base64,${o.agency.qrCode.base64Image}`,alt:"qrcode"},null,8,de)])]),s("div",ce,[s("button",{type:"button",onClick:l[8]||(l[8]=n=>{o.dialogVisible=!0}),class:"btn bg-blue-600 text-white"},[o.loading?(m(),u("i",ie)):b("",!0),f(" "+a(e.$t("change_password")),1)]),s("div",null,[o.issetAgency?(m(),u("button",re,[o.loading?(m(),u("i",me)):b("",!0),f(" "+a(e.$t("update")),1)])):(m(),u("button",ue,[o.loading?(m(),u("i",be)):b("",!0),f(" "+a(e.$t("create_site")),1)]))])])]),_:1},8,["onSubmit"])])])])])]),c(v,{modelValue:o.dialogVisible,"onUpdate:modelValue":l[13]||(l[13]=t=>o.dialogVisible=t),class:"md:w-20",title:e.$t("change_password"),draggable:""},{default:g(()=>[c(w,{class:"w-full",onSubmit:y.onSubmitChange},{default:g(({errors:t})=>[s("div",he,[s("div",pe,[s("label",ge,a(e.$t("currentPassword")),1),c(i,{type:"password",modelValue:o.changePassword.currentPassword,"onUpdate:modelValue":l[9]||(l[9]=n=>o.changePassword.currentPassword=n),as:"input",rules:"required",class:d(["form-control",{"is-invalid":t.name}]),name:"currentPassword",placeholder:e.$t("placeholder")+e.$t("currentPassword")},null,8,["modelValue","class","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.currentPassword}])},a(t.currentPassword),3)]),s("div",fe,[s("label",ye,a(e.$t("newPassword")),1),c(i,{type:"password",modelValue:o.changePassword.newPassword,"onUpdate:modelValue":l[10]||(l[10]=n=>o.changePassword.newPassword=n),as:"input",rules:"required",class:d(["form-control",{"is-invalid":t.name}]),name:"newPassword",placeholder:e.$t("placeholder")+e.$t("newPassword")},null,8,["modelValue","class","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.newPassword}])},a(t.newPassword),3)]),s("div",we,[s("label",ve,a(e.$t("confirmPassword")),1),c(i,{type:"password",modelValue:o.changePassword.confirmPassword,"onUpdate:modelValue":l[11]||(l[11]=n=>o.changePassword.confirmPassword=n),as:"input",rules:"required",class:d(["form-control",{"is-invalid":t.name}]),name:"confirmPassword",placeholder:e.$t("placeholder")+e.$t("confirmPassword")},null,8,["modelValue","class","placeholder"]),s("div",{class:d(["invalid-feedback",{block:t.confirmPassword}])},a(t.confirmPassword),3)])]),s("span",ke,[s("button",{onClick:l[12]||(l[12]=n=>o.dialogVisible=!1),type:"button",class:"btn border bg-white-600 mr-3"},a(e.$t("cancel")),1),s("button",_e,a(e.$t("change")),1)])]),_:1},8,["onSubmit"])]),_:1},8,["modelValue","title"])],64)}var Ue=P(N,[["render",Ve]]);export{Ue as default};
