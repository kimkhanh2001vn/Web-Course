var sas=sas||{};sas.utils=sas.utils||{},sas.events=sas.events||{},sas.utils.extend=function(t){for(var e=1;e<arguments.length;e++){var a=arguments[e];if(a&&"object"==typeof a)for(var n in a)void 0!==a[n]&&(Array.isArray(a[n])?t[n]=a[n]:"object"==typeof a[n]?t[n]=sas.utils.extend({},t[n],a[n]):t[n]=a[n])}return t},sas.utils.Latch=function(t){for(var e=[],a={},n=t=t||[],s=!1,r=0;r<n.length;r++)a[t[r]]={};var i=function(){if(!s){for(var t in a)if(!a[t].status)return;s=!0;for(var n=o(),r=0;r<e.length;r++)e[r].apply(this,n)}},o=function(){for(var t=[],e=0;e<n.length;e++)t.push(a[n[e]].result);return t};this.isComplete=function(){return s},this.notify=function(t,e){a[t]&&(a[t].status=!0,a[t].result=e,i())},this.addListener=function(t){null!=t&&(s?t():e.push(t))},i()},sas.utils.getIEVersion=function(){var t=navigator.userAgent.match(/(?:MSIE |Trident\/.*; rv:)(\d+)/);return t?parseInt(t[1]):void 0},sas.utils.parseCookies=function(t){for(var e={},a=t.split("; "),n=0;n<a.length;n++){var s=a[n],r=s.indexOf("="),i=s.substring(0,r),o=s.substring(r+1);e[i]=o}return e},sas.utils.setCookie=function(t,e,a,n){var s=new Date;s.setTime(s.getTime()+24*n*60*60*1e3);var r="expires="+s.toUTCString();t.cookie=e+"="+a+";"+r+";path=/"},sas.events.addEvent=function(t,e,a){if(t&&e&&a)return t.attachEvent?t.attachEvent("on"+e,a):t.addEventListener&&t.addEventListener(e,a,!1),{removeEvent:function(){t.detachEvent?t.detachEvent("on"+e,a):t.removeEventListener&&t.removeEventListener(e,a,!1)}}},sas.events.addLoadEvent=function(t,e){if(t&&e){var a="load",n=function(){return!0};(sas.utils.getIEVersion()<11||t==document)&&(a="readystatechange",n=function(){if(!t.readyState||"complete"==t.readyState||"loaded"==t.readyState)return!0});var s=sas.events.addEvent(t,a,function(){n()&&(s.removeEvent(),e.apply(this,arguments))})}},sas.events.addMessageEvent=function(t,e){t&&e&&sas.events.addEvent(t,"message",function(){e.apply(this,arguments)})},sas.events._events=sas.events._events||{},sas.events._history=sas.events._history||[],sas.events.on=function(t,e,a){sas.events._events[t]=sas.events._events[t]||{que:[]};var n=sas.events._events[t];a&&(n=n[a]=n[a]||{que:[]}),n.que.push(e)},sas.events.fire=function(t,e,a){sas.events._history.push({eventName:t,data:sas.utils.extend({},e),id:a});var n=sas.events._events[t];if(n){if(a&&n[a])for(var s=0;s<n[a].que.length;s++)n[a].que[s](sas.utils.extend({},e),a);for(s=0;s<n.que.length;s++)n.que[s](sas.utils.extend({},e),a)}},sas.events.off=function(t,e,a){var n=sas.events._events[t];if(n){var s=n.que;if(a&&(s=n[a].que),s){var r=s.indexOf(e);r>=0&&s.splice(r,1)}}},sas.events.history=function(t){for(var e=[],a=0;a<sas.events._history.length;a++){var n=sas.events._history[a];t&&n.eventName!=t||e.push(sas.utils.extend({},n))}return e},function(t,e){if(!sas.__smartLoaded){sas.__smartLoaded=!0;var a=function(t){var e=this;e.instances=[],sas.events.on("load",function(){e._onLoad.apply(e,arguments)},t),sas.events.on("noad",function(){e._onNoad.apply(e,arguments)},t),sas.events.on("error",function(){e._onError.apply(e,arguments)},t),sas.events.on("beforeRender",function(){e._beforeRender.apply(e,arguments)},t)};a.prototype.onCall=function(t,e,a){this.callType=t,this.ad=e,this.options=a},a.prototype.reset=function(t,e){if(this.displayData=null,t?this.renderStatus=null:"rendered"==this.renderStatus&&(this.renderStatus="pending"),sas.events.fire("reset",{formatId:this.ad.formatId,instance:this.ad.instance,tagId:this.ad.tagId,hardReset:t},this.ad.tagId),e)for(var a=0;a<this.instances.length;a++)this.instances[a].reset(t)},a.prototype.setHeaderBiddingWinner=function(t){this.headerBidding=t},a.prototype._onLoad=function(){var a=e.getElementById(this.ad.tagId);if(a){var n=a.childNodes.length>1;t.sas_loadHandler&&t.sas_loadHandler({id:this.ad.formatId,hasAd:n}),this.options&&this.options.onLoad&&this.options.onLoad({formatId:this.ad.formatId,tagId:this.ad.tagId,hasAd:n})}},a.prototype._beforeRender=function(t){this.options&&this.options.beforeRender&&this.options.beforeRender(t)},a.prototype._onError=function(t){this.options&&this.options.onError&&this.options.onError(t)},a.prototype._onNoad=function(t){this.callType==sas.callType.ONECALL&&this.ad.isOnecallJSON?this.displayData.scriptNoad&&((new Image).src=this.displayData.scriptNoad):this.callType==sas.callType.ONECALL&&sas_manager&&sas_manager.noad(this.ad.formatId),this.options&&this.options.onNoad&&this.options.onNoad(t)},a.prototype.clean=function(){var t=e.getElementById(this.ad.tagId);this.options&&this.options.onClean&&this.options.onClean(this.ad.formatId,t),sas.events.fire("clean",{formatId:this.ad.formatId,instance:this.ad.instance,tagId:this.ad.tagId},this.ad.tagId),t&&(t.innerHTML="")},a.prototype.render=function(){var t=this;if("rendered"!=this.renderStatus)if(this.displayData){this.renderStatus="rendered",this.clean();var a={formatId:this.ad.formatId,instance:this.ad.instance,tagId:this.ad.tagId};sas.events.fire("beforeRender",a,t.ad.tagId);var n=function(){sas.events.fire("load",a,t.ad.tagId)},s=function(){sas.events.fire("error",a,t.ad.tagId),sas.events.fire("noad",a,t.ad.tagId)};if(!this.displayData.scriptSrc&&!this.displayData.content)return sas.events.fire("load",a,this.ad.tagId),void sas.events.fire("noad",a,this.ad.tagId);var r=e.getElementById(this.ad.tagId);if(r){switch(this.displayData.scriptType){case"iframe":k(r,this,this.options.async,n,s);break;case"script":H(r,this.displayData.scriptSrc,this.options.async,n,s);break;case"passback":sas.passback({formatId:this.ad.formatId,tagId:this.ad.tagId,chain:this.displayData.chain});break;case"content":U(r,this),n();break;default:throw new Error("Unsupported script type "+this.displayData.scriptType)}sas.events.fire("render",a,this.ad.tagId)}else sas.events.fire("error",a,this.ad.tagId)}else this.renderStatus="pending"};var n=function(){},s=!1,r=function(){return Math.round(1e10*Math.random())},i="http://www.smartadserver.com",o=r(),d=!0,c=encodeURIComponent,l=decodeURIComponent,p=0,f=!1,u=0,h=!1,g=null,v={},m=[],y={},I={},_={},T=null;sas._networks=sas._networks||{},sas._pendingCommands=sas._pendingCommands||{},sas.callType={STD:"std",IFRAME:"iframe",ONECALL:"onecall",XML:"xml",PASSBACK:"passback"},sas.renderMode={DEFAULT:0,READY:1,ON_DEMAND:2};var E=!1,w=[],S={onLoad:n,onError:n,onClean:n,beforeRender:n};sas.events.on("call",function(t){var e=v[t.ad.tagId];e.displayData={scriptType:"script",scriptSrc:J(t.callType,t.ad,t.options)},z.addListener(function(){e.render()})},sas.callType.STD),sas.events.on("call",function(t){var e=v[t.ad.tagId];e.displayData={scriptType:"iframe",scriptSrc:J(t.callType,t.ad,t.options),width:t.ad.width||0,height:t.ad.height||0},z.addListener(function(){e.render()})},sas.callType.IFRAME),sas.events.on("call",function(t){z.addListener(function(){t.ad.isOnecallJSON?L(t):C(t)})},sas.callType.ONECALL);var C=function(t){var a=J(t.callType,t.ad,t.options)();H(e.getElementsByTagName("head")[0],a,t.options.async,function(){for(var e in sas_manager.formats){if(i=M(e.substring(1))){var a=sas_manager.formats[e];i.options.async||a.chain?i.displayData={scriptType:a.scriptType(),scriptSrc:a.scriptSrc(),chain:a.chain,width:"iframe"==a.scriptType()?a.scriptWidth():0,height:"iframe"==a.scriptType()?a.scriptHeight():0}:i.displayData=tt(a.scriptURL()),i.displayData.scriptSrc&&(i.displayData.scriptSrc=function(t){return function(){return dt(t)}}(i.displayData.scriptSrc))}}var n={};for(var s in t.ad.formats)n[t.ad.formats[s].id]=!0;for(var r in v){var i;n[(i=v[r]).ad.formatId]&&(i.displayData=i.displayData||{},"pending"==i.renderStatus&&i.render())}},t.options.onError)},L=function(t){z.addListener(function(){var e=j(t.ad,t.options);!function(t,e,a,n,s){var r=new XMLHttpRequest;r.onreadystatechange=function(){if(4==this.readyState){var t=JSON.parse(this.responseText);200==this.status?a(t):n(t)}},r.withCredentials=!0;var i=!0;try{r.open("POST",t,!0)}catch(s){if(i=!1,sas.utils.getIEVersion()<=9&&-2147024891==s.number){var o=new XDomainRequest;o.open("POST",t),o.ontimeout=function(){n()},o.onerror=function(){n()},o.onload=function(){var t=JSON.parse(this.responseText);a(t)},o.send(JSON.stringify(e))}}i&&(r.setRequestHeader("content-type","application/json"),r.send(JSON.stringify(e)))}(q(t.ad,t.options),e,function(t){for(var e in t){var a=M(e);if(a){var n=t[e];a.displayData={scriptType:n.ScriptType,scriptSrc:n.ScriptSrc,scriptNoad:n.ScriptNoad,contentType:n.ContentType,content:n.Content,width:n.ScriptWidth,height:n.ScriptHeight},a.displayData.scriptSrc&&(a.displayData.scriptSrc=function(t){return function(){return dt(t)}}(a.displayData.scriptSrc)),"pending"==a.renderStatus&&a.render()}}},t.options.onError,t.options.domain)})},D={},A="invalid site id",N="invalid page id or name",x="invalid format id",b=function(t){throw new Error(t)};Array.isArray||(Array.isArray=function(t){return"[object Array]"===Object.prototype.toString.call(t)});var M=function(t){var e=v[t=""+t]||v["sas_"+t];if(e)return e;var n=t.split("_"),s=n.slice(0,n.length-1).join("_");if(n.length>1&&!isNaN(n[n.length-1])&&(e=v[s]||v["sas_"+s])){var r=parseInt(n[n.length-1]),i=e.ad.tagId+"_"+r,o=new a(i);return o.onCall(e.callType,sas.utils.extend({},e.ad,{tagId:i,instance:r}),e.options),e.instances.push(o),v[i]=o,o}return null},k=function(t,a,n,s,r){var i=e.createElement("iframe");i.id="sas_i_"+a.ad.formatId+(a.ad.instance?"_"+a.ad.instance:""),i.scrolling="no",i.frameBorder=0,i.width=a.displayData.width,i.height=a.displayData.height,i.style.margin=0,i.style.padding=0,i.style.width=a.displayData.width+"px",i.style.height=a.displayData.height+"px",i.src="function"==typeof a.displayData.scriptSrc?a.displayData.scriptSrc():a.displayData.scriptSrc,n?(sas.events.addLoadEvent(i,s),sas.events.addEvent(i,"error",r)):(i.setAttribute("onload",F(s)+"()"),i.setAttribute("onError",F(r)+"()")),a.options.async?t.appendChild(i):e.write(i.outerHTML)},O=1,R=function(t,a,n){var s=e.createElement("script");s.id="sas_script"+O++,s.type="text/javascript",s.text=a,n?t.appendChild(s):e.write(s.outerHTML)},U=function(t,a){switch(a.displayData.contentType){case"application/javascript":R(t,a.displayData.content,a.options.async);break;case"text/html":!function(t,a){var n=e.createElement("iframe");n.id="sas_i"+a.ad.formatId+(a.ad.instance?"_"+a.ad.instance:""),n.scrolling="no",n.frameBorder=0,n.width=a.displayData.width,n.height=a.displayData.height,n.style.margin=0,n.style.padding=0,n.style.width=a.displayData.width+"px",n.style.height=a.displayData.height+"px",t.appendChild(n),n.contentWindow.document.open("text/html","replace"),n.contentWindow.document.write(a.displayData.content),n.contentWindow.document.close()}(t,a)}},B=1,F=function(e){var a="__sas_gcbk_"+B++;return t[a]=function(){t[a]&&(t[a]=void 0,e())},a},H=function(t,a,n,s,r){var i=e.createElement("script");i.id="sas_script"+O++,i.type="text/javascript",i.src="function"==typeof a?a():a,i.async=n,n?(sas.events.addLoadEvent(i,s),sas.events.addEvent(i,"error",r)):(i.setAttribute("onload",F(s)+"()"),i.setAttribute("onError",F(r)+"()")),n?t.appendChild(i):e.write(i.outerHTML)},j=function(e,a){a=sas.utils.extend({forceMasterFlag:!1},a),d=!!a.forceMasterFlag||d,o=a.resetTimestamp?r():o,W();var n=P(),s=ot()||t.location.origin+t.location.pathname,i=screen.height,c=screen.width;return function(t,e,a,n,s,r,i){var o={timestamp:a,networkId:s.networkId,getAdContent:s.getAdContent,siteId:t.siteId,pageId:t.pageId,pageName:t.pageName,master:e,noAdCallback:"sas.noad",pageUrl:n,screen:{height:r,width:i},uid:g||0,noCookie:s.noCookie,ads:[]},d=nt();d&&(o.gdpr_consent=d);for(var c=0;c<t.formats.length;c++){var l=t.formats[c],p=l.tagId?l.tagId:"sas_"+l.id,f=_[p]||{};isNaN(f.cpm)&&(f={}),o.ads.push({formatId:l.id,tagId:p,bidfloor:l.overriddenBidfloor,target:t.target,currency:l.currency,headerBidding:f})}return o}(e,n,o,s,a,i,c)},q=function(t,e){return e.domain+"/"+ +e.networkId+"/call"},J=function(e,a,n){n=sas.utils.extend({forceMasterFlag:!1},n),d=!!n.forceMasterFlag||d,o=n.resetTimestamp?r():o,e==sas.callType.ONECALL&&W();var s=P(),i=ot()||t.location.origin+t.location.pathname,c=screen.height,l=screen.width;return function(){return sas.utils.buildUrl(n.domain,e,a,s,o,n.async,n.networkId,g,i,I,_[a.tagId],n.clickTrackingUrl,n.clickTrackingEncodingLevel,c,l)}},P=function(){return W()?"m":"s"},W=function(){return!!d&&(d=!1,!0)},V=function(t){var a,n,s=e.getElementById(t);return s||(e.write((a=t,n=e.createElement("div"),n.id=a,n).outerHTML),s=e.getElementById(t)),s},X=[];sas.events.addLoadEvent(e,function(){s=!0,X.push=function(t){t()};for(var t=0;t<X.length;t++)X[t]()});var Y=!1;sas.events.addMessageEvent(t,function(){if(arguments&&!(arguments.length<1)){var e=arguments[0];if(e&&e.data&&"string"==typeof e.data&&!(e.data.indexOf("SMRT")<0)){var a=e.data.split(" ");if(!(a.length<2)){var n=a[2]||"",s=a[1].split(".");if(0!==s.length){for(var r=t,i=0;i<s.length;i++)if(!(r=r[s[i]]))return;try{r(n)}catch(t){}}}}}});var G,K=function(t,e){if(!t)return!1;if(!e)return!1;var a,n=sas._networks[e.networkId];return!!(n&&n.f&&n.f[t.formatId]&&(a=n.f[t.formatId],Math.random()<a))},$=!1,z=new sas.utils.Latch(["loaded"]);sas.setup=function(e){if($)throw new Error("sas.setup can only be called once");$=!0,i=(e=e||{}).domain||"http://www.smartadserver.com",f=e.async||f,sas_ajax=f,p=e.networkid||p,u=e.renderMode||u,h=e.inSequence||h,g=e.uid,I=sas.utils.extend(I,e.partnerIds),t.sas_renderMode=u,sas.configure({id:p}),f&&u!=sas.renderMode.DEFAULT||z.notify("loaded"),u==sas.renderMode.ON_DEMAND&&(G=setTimeout(function(){sas.render()},parseInt(e.renderModeTimeout)||5e3))},sas.call=function(t,e,n){"string"!=typeof t&&(n=sas.utils.extend({},e,{async:!0}),e=t,t="std",u==sas.renderMode.DEFAULT&&z.notify("loaded")),n=sas.utils.extend({},n),t==sas.callType.ONECALL&&e.formats&&(n.async=!0),(e=sas.utils.extend({},e)).siteId=e.siteId||e.siteid,e.pageId=e.pageId||e.pageid,e.pageName=e.pageName||e.pagename,e.formatId=e.formatId||e.formatid,e.siteId||b(A);var s,r,o=(s=navigator.userAgent.indexOf("iPad")>0,r=""+navigator.userAgent.indexOf("iPhone")>0,s||r?s?"ipad":"iphone":navigator.userAgent.indexOf("Android")>0?"android":"");if(o.length>0&&(e.target&&e.target.length>0&&(e.target+=";"),e.target+="platform="+o),e.pageId||e.pageName||b(N),e.formatId||e.formats||b(x),(n=sas.utils.extend({},S,{async:f,domain:i,networkId:p},n))&&n.networkId){var d=parseInt(n.networkId);if(d>0&&!sas._networks[d])return sas._pendingCommands[d]=sas._pendingCommands[d]||[],void sas._pendingCommands[d].push(function(){sas.call(t,e,n)})}if(!K(e,n)){if(!e||!e.siteId||!e.pageId&&!e.pageName||!e.formatId&&!e.formats)throw new Error("Missing parameter(s)");if(t==sas.callType.ONECALL){if(e.isOnecallJSON=!!e.formats,!e.formats){e.formats=[];for(var c=(""+e.formatId).replace(/\s/g,"").split(","),l=0;l<c.length;l++)e.formats.push({id:c[l]})}for(l=0;l<e.formats.length;l++){var g=e.formats[l];g.tagId=g.tagId||"sas_"+g.id,(y=v[g.tagId]||new a(g.tagId)).onCall(t,sas.utils.extend({},e,{tagId:g.tagId,formatId:g.id,originalFormatId:g.id}),n),y.reset(!!n.reset,!0),v[g.tagId]=y}}else{var y;e.tagId=e.tagId||"sas_"+e.formatId,V(e.tagId),(y=v[e.tagId]||new a(e.tagId)).onCall(t,sas.utils.extend({},e),n),y.reset(!!n.reset),v[e.tagId]=y}n.async&&u==sas.renderMode.READY&&(Y||(Y=!0,X.push(function(){sas.render()}))),e.passback||m.push({callType:t,ad:sas.utils.extend({},e),options:sas.utils.extend({},n)}),n.async&&h?z.addListener(function(){var a,s;a={callType:t,ad:e,options:n},s=function(){if(w.length>0){E=!0;var t=w.shift(),e=t.options.onLoad;t.options.onLoad=function(t){s(),e(t)},sas.events.fire("call",{callType:t.callType,ad:t.ad,options:t.options},t.callType)}else E=!1},w.push(a),E||s()}):sas.events.fire("call",{callType:t,ad:e,options:n},t)}},sas.passback=function(t){y[t.formatId]={current:-1,ad:t};var e=M(t.formatId);if(e.options.onNoad){var a=e.options.onNoad;e.options.onNoad=function(e){(new Image).src=t.noadUrl,a({formatId:e.formatId,tagId:e.tagId})}}else e.options.onNoad=function(){(new Image).src=t.noadUrl};sas.next(t.formatId)},sas.next=function(t){var a=M(t),n=y[a.ad.formatId];if(a&&n){var s=e.getElementById(a.ad.tagId);s&&(s.innerHTML=""),n.current>=0&&((new Image).src=n.ad.chain[n.current].noadUrl),n.current++,n.current<n.ad.chain.length&&(n.ad.chain[n.current].countUrl&&((new Image).src=n.ad.chain[n.current].countUrl),n.ad.chain[n.current].script?R(s,n.ad.chain[n.current].script,a.options.async):H(s,n.ad.chain[n.current].scriptUrl,a.options.async))}};var Q={forceMasterFlag:!1,resetTimestamp:!0,target:void 0};sas.refresh=function(t,e){if(z.isComplete()){e=sas.utils.extend({},Q,t,e);var a=n;if(arguments.length<=1&&"string"!=typeof t&&isNaN(t))a=function(t){for(var a in v)v[a].reset();for(a=0;a<m.length;a++)m[a].options.async&&(t&&(m[a].ad.target=t),sas.events.fire("call",{callType:m[a].callType,ad:sas.utils.extend({},m[a].ad,{target:t}),options:sas.utils.extend({},m[a].options,e)},m[a].callType))};else{var s=M(t);if(!s||!s.options.async)return;s.reset(),a=function(t){void 0!==t&&null!==t&&(s.ad.target=t||s.ad.target),sas.events.fire("call",{callType:sas.callType.STD,ad:s.ad,options:s.options},sas.callType.STD)}}d=!!e.forceMasterFlag||d,o=e.resetTimestamp?r():o,a(e.target)}},sas.getTag=function(t){var a=M(t);return a?e.getElementById(a.ad.tagId):null},sas.clean=function(t){if(0==arguments.length)for(var e in v){var a;(a=v[e]).clean()}else(a=M(t))&&a.clean()},sas.noad=function(t){var e=M(t);e&&sas.events.fire("noad",{formatId:e.ad.formatId,instance:e.ad.instance,tagId:e.ad.tagId},e.ad.tagId)},sas.render=function(t){if(0==arguments.length){if(clearTimeout(G),!z.isComplete()&&(u==sas.renderMode.READY&&s||u==sas.renderMode.ON_DEMAND))for(var e in z.notify("loaded"),v){var a;"pending"!=(a=v[e]).renderStatus||!a.options.async&&s||a.render()}}else(a=M(t))&&(!a.options.async&&s||V(a.ad.tagId),a.render())},sas.setPartnerId=function(t,e){I[t]=e},sas.appendHtml=function(t,a){var n=e.getElementById(t);if(Range&&Range.prototype.createContextualFragment)n.appendChild(e.createRange().createContextualFragment(a));else{var s=e.createElement("div");s.innerHTML=a;for(var r=s.childNodes,i=0;i<r.length;i++)n.appendChild(it(r[i]))}},sas.configure=function(t){if(t&&t.id&&!(parseInt(t.id)<=0)&&!sas._networks[t.id]&&(sas._networks[t.id]=t,sas._pendingCommands[t.id]))for(var e;e=sas._pendingCommands[t.id].shift();)e()},sas.setHeaderBiddingWinner=function(t,e){_[t]=e},sas.setGdprConsentData=function(t){T=t},sas.utils.buildUrl=function(t,e,a,n,s,r,i,o,d,l,p,f,u,h,g){var v={};if(null!=i&&(v.nwid=i),v.siteid=a.siteId,v.pgid=a.pageId,v.pgname=a.pageName,v.fmtid=a.formatId,e==sas.callType.IFRAME&&(v.out="iframe"),r&&(v.async=1),e!=sas.callType.ONECALL){var m=n.split("=");v.visit=m.length>1?m[1]:n}else v.oc=1;for(var y in v.tmstp=s,a.target&&(v.tgt=c(a.target)),e!=sas.callType.ONECALL&&(v.orgfmtid=a.originalFormatId,v.tag=a.tagId),o&&(v.uid=o),h&&(v.sh=h),g&&(v.sw=g),d&&(v.pgDomain=c(d)),p&&e!=sas.callType.ONECALL&&(v.hb_bid=p.bidder,v.hb_cpm=p.cpm,v.hb_ccy=p.currency,v.hb_dealid=p.dealId),f&&(v.clcturl=c(f)),u&&(v.clctenc=clickTrackingEncoding),l)v[encodeURIComponent("extuid-"+y)]=encodeURIComponent(l[y]);var I=nt();I&&(v.gdpr_consent=I),v.noadcbk="sas.noad";var _=[];for(var T in v)null!=v[T]&&_.push(T+"="+v[T]);return t+"/ac?"+_.join("&")};var Z=function(t,e,a){var n=t.indexOf("/");if(n<0)throw new Error("Invalid argument : sas_pageid");this.siteId=t.substring(0,n);var s=t.substring(n+1);0==s.indexOf("(")&&s.indexOf(")")==s.length-1&&(s=s.slice(1,s.length-1));var r=parseInt(s);r+""==s?this.pageId=r:this.pageName=s,this.formatId=e,this.target=a};t.sas_ads=sas,t.sas_ajax=!1,t.sas_manager=null,t.sas_unrenderedFormats=[],t.sas_callAd=sas.callAd,t.sas_callAds=sas.callAds,sas.callAd=sas.refresh,sas.callAds=function(){z.isComplete()?sas.refresh():sas.render()},sas.cleanAd=sas.clean,sas.cleanAds=sas.clean;var tt=function(t){var a=e.createElement("div");a.innerHTML="_"+t;var n=a.childNodes[1];return{scriptType:n.tagName.toLowerCase(),scriptSrc:n.src,width:n.width||0,height:n.height||0}};t.sas_render=function(t,e,a,n,s){sas.render(t)},t.SmartAdServerAjaxOneCall=function(t,e){sas.render(e)},t.SmartAdServer_iframe=function(t,e,a,n,s){var r=new Z(t,e,a);r.width=n,r.height=s,sas.call("iframe",r)},t.SmartAdServer=function(t,e,a){var n=new Z(t,e,a);sas.call(sas.callType.STD,n)},t.SmartAdServerAjax=SmartAdServer,t.sas_gcf=function(t){return e.getElementById("sas_"+t)},t.sas_appendToContainer=function(t,a){var n=e.getElementById("sas_"+t);if(n){if("string"==typeof a){var s=e.createElement("div");s.innerHTML=a,a=s}n.appendChild(a)}},t.sascc=function(t,e){(new Image).src=i+"/h/mcp?imgid="+t+"&pgid="+e+"&uid=[uid]&tmstp="+o+"&tgt=[targeting]"},t.sasmobile=function(t,e,a){var n=new Z(t,e,a);sas.call(sas.callType.STD,n)};var et={};t.sas_addCleanListener=function(t,e){et[t]=e},sas.events.on("clean",function(t){et[t.tagId]&&et[t.tagId](),et[t.formatId]&&et[t.formatId]()}),t.sas_cleanAds=sas.clean,t.sas_cleanAd=sas.clean,void 0!==t.__cmp&&"[object Function]"===Object.prototype.toString.call(t.__cmp)?t.__cmp("getConsentData",null,rt):function(){for(var e,a,n=t;!e;){try{n.frames.__cmpLocator&&(e=n)}catch(a){}if(n===t.top)break;n=n.parent}t.__cmp=function(t,n,s){if(!e)return a.removeEvent(),void(void 0!=s&&s({msg:"CMP not found"},!1));var r="smartjs"+Math.random(),i={__cmpCall:{command:t,parameter:n,callId:r}};D[r]=s,e.postMessage(i,"*")},a=sas.events.addEvent(t,"message",st),t.__cmp("getConsentData",null,function(t,e){a.removeEvent(),rt(t,e)})}(),sas.cmd=sas.cmd||[],sas.cmd.push=function(t){t()};for(var at=0;at<sas.cmd.length;at++)sas.cmd[at]()}function nt(){if(T)return T;var t=sas.utils.parseCookies(e.cookie);return t?t.sas_euconsent:null}function st(t){var e="string"==typeof t.data&&-1!=t.data.indexOf("cmpReturn")?JSON.parse(t.data):t.data;if(e.__cmpReturn){var a=e.__cmpReturn;D[a.callId](a.returnValue,a.success),delete D[a.callId]}}function rt(t,a){a&&t&&t.consentData&&sas.utils.setCookie(e,"sas_euconsent",t.consentData,30)}function it(t){var a;if("script"==t.tagName.toLowerCase())a=e.createElement("script"),t.type&&(a.type=t.type),t.src&&(a.src=t.src),t.text&&(a.text=t.text);else{a=t.cloneNode(!1);for(var n=t.childNodes,s=0;s<n.length;s++)a.appendChild(it(n[s]))}return a}function ot(){var e=t;try{for(;e.parent.document;){if(e.parent.document===e.document)return e.location.origin+e.location.pathname;e=e.parent}}catch(t){}try{try{if(t.top.location.href)return t.top.location.origin+t.top.location.pathname}catch(t){}var a=t.location.ancestorOrigins;return 1==a.length?e.document.referrer:a[a.length-1]}catch(t){return e.document.referrer}}function dt(t){var a,n=e.createElement("a"),s=/(?:^\?|&)([^=&]+)=?([^&]*)(?=&|$)/g;n.href=t;for(var r=n.search,i=[];null!==(a=s.exec(r));){"extuid-"===(p={n:l(a[1]),v:l(a[2])}).n.substr(0,7)&&I[p.n.substr(7)]||i.push(p)}for(var o in I)i.push({n:"extuid-"+o,v:I[o]});r="";for(var d=0;d<i.length;d++){var p=i[d];r+=(0===d?"?":"&")+c(p.n),""!==p.v&&(r+="="+c(p.v))}return n.search=r,n.href}}(window,document);if(window.sas && sas.configure){sas.configure({id:2060});}