  <template>
      <div>
          
       <div class="row" style="margin-top:10px">         
           <div class="col-md-2" style="position:absolute;top:0px; left:83%">
               <div class="row" style="color:#fff; float:right">

                   <ul class="navbar" style="list-style:none">
                       <li class="nav-item item">
                           <a v-on:click="showForm" style="cursor:pointer"><i class="icofont-options"></i></a>
                       </li>
                       <li class="nav-item item">
                           <a v-on:click="newAttribute" style="cursor:pointer"><i class="icofont-plus"></i></a>
                       </li>
                       <li class="nav-item item">
                           <div class="btn-group">
                               <a style="cursor: pointer;" class="" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                   <i class="fa fa-ellipsis-v"></i>
                               </a>
                               <div class="dropdown-menu dropdown-menu-right">
                                   <button class="dropdown-item" type="button">Duplicate</button>
                                   <button v-on:click="Remove" class="dropdown-item" type="button">Delete</button>
                                   <button v-on:click="showForm" class="dropdown-item" type="button">Edit</button>
                                   <button v-on:click="publish" class="publish dropdown-item" type="button">Publish</button>
                               </div>
                           </div>
                       </li>
                   </ul>
                  
               </div>              

           </div>
           </div>  
          
          <div class="row">
              <div class="col-md-4">                 
                  <div class=' list' style='border-right:1px solid #ccc'>
                      <h4 class=''>Attribute list</h4><ul>
                          <li v-for="d in attri.Attributes"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="ShowAttribute(d.FieldID)">{{d.AttributeName}} </a></div>
                      </ul>
                  </div>
              </div>
              <div class="col-md-8" >
                  <router-view :key="$route.fullPath" :onAdd="AfterAdd"></router-view>
              </div>
          </div>
          <div id="window"></div>
      </div>
    
</template>
  <script>
        var aid = "";
        var cid = "";
        var attr = {};
        module.exports = {
        data: function () {
                return {
                    appid: "",
                    compid:"",
                attri: attr
            }
        },
        updated: function () {

            },
            beforeRouteUpdate(to, from, next) {
                aid = to.params.id;
                cid = to.params.cid;
                myComp(to.params.id, to.params.cid, function (data) {
                    next();
                })
            },
            beforeRouteEnter(to, from, next) {
                aid = to.params.id;
                cid = to.params.cid;
        myComp(to.params.id, to.params.cid, function (data) {
            next();
        })
    },mounted:function(){
         if (this.attri.State == 1){
      $(".btn-group").find(".publish").addClass("disabled");
      }
      },
            created: function () {
     //debugger;

                this.appid = this.$route.params.id;
                this.compid = this.$route.params.cid;
                this.getAttributes(this.appid, this.compid);
                $("#apptitle").find("h5").css({ "float": "left" });
        $("#apptitle").find("h5").html("/"+ this.attri.ComponentName);
           // this.getAttributes(this.$route.params.id, this.$route.params.cid);
        },
            methods: {
      publish:function(){
      var that = this;
                $.ajax('/App/' + this.appid + '/Component/' + this.compid + "/publish",
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        success: function (data, status, xhr) {   // success callback function

                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
      },

                AfterAdd: function () {
                    this.getAttributes(this.appid,this.compid);
                },
            getAttributes: function (appid,id) {
                var that = this;
                $.ajax('/App/' + appid + '/Component/' + id,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        success: function (data, status, xhr) {   // success callback function
                            that.attri = data;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            newAttribute: function () {
                this.$router.push({
                    path: "/Home/" + this.appid + "/attributes/" + this.compid + "/addattribute",
                    params: { id: this.appid, cid: this.compid }, props: true
                })
                //this.$router.push("/Home/" + this.appid + "/attributes/" + this.compid + "/addattribute");
            },
            showForm: function () {
       

                this.$router.push({ path: "/Home/"+ this.appid +"/component/" + this.compid, params: { id: this.appid, cid: this.compid, rd: + Math.random() * 100001 } });
                // this.$router.push({ path: "view" });
                //this.$router.push("/App/" + this.Appid + "/" + this.compid + "/view/" + Math.random() * 100001, { params: { id: this.Appid, action: this.compid } });
                //this.$router.push("/App/" + this.Appid + "/" + this.compid + "/profile/b/"+ Math.random() * 100001);
                //  this.$router.push("/App/" + this.Appid + "/" + this.compid + "/b/" + Math.random() * 100001);
            },
      Remove:function(){
      var that=this;
    $("#window").dialogBox({
                            id: 'wd',
                            title: "",
                            message: "Are you sure, you want to remove this Component?",
                            onAction: function () {
          $("#window").dialogBox('hide');
                          $.ajax('/App/' + that.appid + '/Component/Remove/' + that.compid,
                {
                    type: "GET",
                    dataType: 'jsonp', // type of response data
                    //  data: { clientid: id },
                    // timeout milliseconds
                    success: function (data, status, xhr) {   // success callback function
      localStorage.removeItem("CompList");
      that.$router.go(-1);
       Window.toz.$refs.foo.push({
                                header: "", body: "Component removed successfully", template: ""
                                , autohide: true,
                                bodyCss: "feedback",
                                position: "top right",
                                autohide: true,
                            });
                    },
                    error: function (jqXhr, textStatus, errorMessage) { // error callback
                        alert(errorMessage);
                    }
                });


                            },
                            onCancel: function () {
                                //    alert('no');
                            },
                            type: 2,
                            alertType: 1,
                        });
                        $("#window").dialogBox('show');
      },
        ShowAttribute:function(aid){
         this.$router.push({
                    path:   "/Home/"+ this.appid+"/attributes/"+this.compid+"/change/" + aid,
                    params: { id: this.appid, cid: this.compid, attid:aid}, props: true
                })
      },
        }
        }

        //var rec=  Vue.compile(template);
        var myComp = function (appid, cmpid, next) {
            var sthis = {};
            // this._data.title = "sampathkumar";
            $.ajax('/App/' + appid + '/Component/' + cmpid,
                {
                    type: "GET",
                    dataType: 'jsonp', // type of response data
                    //  data: { clientid: id },
                    // timeout milliseconds
                    success: function (data, status, xhr) {   // success callback function
                        attr = data;
                        if (next != undefined) { next(data); }

                    },
                    error: function (jqXhr, textStatus, errorMessage) { // error callback
                        alert(errorMessage);
                    }
                });

        }
        var fieldtype = [
            { key: 0, value: "Number" },
            { key: 1, value: "Decimal" },
            { key: 2, value: "Text" },
            { key: 3, value: "LongText" },
            { key: 4, value: "Currency" },
            { key: 5, value: "LookUp" },
            { key: 6, value: "Component LookUp" },
            { key: 7, value: "File" },
            { key: 8, value: "Picture" },
            { key: 9, value: "Date" },
            { key: 10, value: "Time" },
            { key: 11, value: "Date & time" },
            { key: 12, value: "Boolean" },
            { key: 13, value: "Year" },
            { key: 14, value: "Month" },
            { key: 15, value: "Quarter" },
        ]


  </script>
  
