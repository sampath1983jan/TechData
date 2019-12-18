<template id="ComponentManager">
    <div id="appView">
        <!--Slider Start-->
        <div id="slider" class="hidden">
            <div class="row">
                <comp-list :appid="Appid" v-on:onComponentChange="onChanged" ref="refComp"></comp-list>
                <div class="col-md-3" id="formContainer"></div>
                <div class="col-md-3" id="pageContainer"></div>
            </div>
        </div>
        <!--Slider End-->        
        <div>
            <div id="itemlist">
                <div class="row">
                    <div class="col-md-4">
                        <div class=' list' style="border-radius:5px;border-style: solid;border-width: 1px 1px 1px 1px;border-color:#ccc">
                            <div class=''  style=" background-color:#ff006e; padding:5px; color:#fff;border-radius:5px">Components</div>
                            <ul>
                                <li v-for="d in Complist"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="onChanged(d.ComponentID)">{{d.ComponentName}} </a></div>
                            </ul>
                            <div style="margin:5px 15px; text-align:right"><a v-on:click="showFullComponent">More ...</a></div>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
        <router-view :key="$route.fullPath"></router-view>        
    </div>
</template>
<script>
    var moment = {};
    var a = httpVueLoader('/vue/App/Component/ComponentList.vue?3' + (Math.random() * 10000));
    var CompForm = httpVueLoader('/vue/App/Component/CompForm.vue?' + (Math.random() * 10000));  
    var cdetail = {};
    var myComp = function (next,fn) {
        var sthis = {};       
       // this._data.title = "sampathkumar";
        $.ajax('/App/' + this.AppID + '/Component/' + this.CompID,
            {
                type: "GET",
                dataType: 'jsonp', // type of response data            
                success: function (data, status, xhr) {   // success callback function    
                    cdetail = data;                  
                    next(fn);
                },
                error: function (jqXhr, textStatus, errorMessage) { // error callback
                    alert(errorMessage);
                }
            });
    } 
    module.exports = {
        name: "componentmanager",
        components: {
            'comp-list': a,
            'comp-form': CompForm 
        },
        data: function () {
            return {
                Appid: this.name,
                Complist: [],
                refreshList: true,
                compid: "",
            }
        }, beforeRouteEnter(to, from, next) {
            if (to.name == "component") {
                $("#itemlist").hide();
            }
            next();
           
        }, beforeRouteUpdate(to, from, next) {
          
            if (to.name == "component") {
                $("#itemlist").hide();
            } else if (to.name == "home" || to.name == undefined) {
                $("#itemlist").show();
            }
            next();
        },
        props: ["name"],
        created: function () {            
            var a = this.$route.params.AppiD;
            var uri = window.location.hash;
            var params = uri.substring(2);
            params = params.split("/");
            this.Appid = this.$route.params.AppiD;
           this. getComps();                
        },
        methods: {
            showFullComponent: function () {
                $('#apptitle')[0].click();
            },
            showForm: function () {               
                this.$router.push({ path: "/foo", params: { id: this.Appid, cid: this.compid, rd: + Math.random() * 100001 }});    
            },
            onChanged: function (id) {              
                this.compid = id;             
                this.$router.push({ path: "/Home/" + this.Appid +"/attributes/"  + this.compid, params: { id: this.Appid, cid: this.compid },props:true });
             //   this.$router.push("/App/" + this.Appid + "/" + id);           
                window.hideMenu();
            },
            getTop: function (no, data) {
                var i = 1;
              
                var d = [];             
                $.each(data, function (ik, v) {
                    if (i > no) {
                        return;
                    } else {
                        d.push(v.Component);
                        i = i + 1;
                    }
                });
                return d;
            },
            getComps: function () {
                var that = this;
                var that = this;
             
                if (localStorage.getItem('CompList') != null) {
                     
                    that.Complist = this.getTop(5, JSON.parse( localStorage.getItem('CompList')));
                }
                $.ajax('/MyApp/' + this.Appid + '/Components',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: {},
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function         
                            var d = [];                   
                            d = that.getTop(5, data);
                            localStorage.setItem('CompList', JSON.stringify( data));
                            that.Complist = d;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            refresh: function () {
                if (this.refreshList == true) {
                    this.refreshList = false;
                }
                else this.refreshList = true;
            }
        },
        //router: router
    }

</script>