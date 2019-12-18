<template>
    <div>
        <h2>Component Information</h2>
        <div style='margin-left:35%'>
            <label class="stlabel">Component Name</label>
            <div style="width:65%">
                <tzinput :attribute="{id:'txtComponentName',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="compName" v-on:input="updating"></tzinput>

            </div>
            <label class="stlabel">Title</label>
            <div style="width:65%">
                <tzinput :attribute="{id:'txtTitle',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="title" v-on:input="updatetitle"></tzinput>
            </div>
            <label class="stlabel">Title</label>
            <div style="width:65%">
                <tzinput :attribute="{id:'txtCagory',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="category" v-on:input="updatecategory"></tzinput>
            </div>
            <label class="stlabel">Title Format</label>
            <div style="width:65%">
                <tzinput :attribute="{id:'txtTitleFormat',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="titleformat" v-on:input="updatetitleformat"></tzinput>
            </div>

            <div style="position:absolute; left:69%">
                <button class="btn btn-primary" v-on:click="Save">Save</button>
                <button class="btn btn-default" v-on:click="Close">Cancel</button>
            </div>
        </div>
        <router-view :key="$route.fullPath" name='b'></router-view>
    </div>
</template>


<script>
   
      var template = "<div style='margin-left:35%'>"
        + '<label class="stlabel"> Component Name</label>'
        + '<div style="width:65%">'
        + '<tzinput :attribute="{'
        + "id: 'txtComponentName', "
        + "isRequired:false,"
        + "enabletooltip:true,"
        + "min:0,"
        + "inputType:1,"
        + "max:500,"
        + "limit:true"
        + ' }"'
        + ':value="compName" v-on:input="updating" ></tzinput > '
     +"</div>"
        + '<label class="stlabel">Title</label>'
        + '<div style="width:65%">'
        + '<tzinput :attribute="{'
        + "id: 'txtTitle',"
        + "isRequired:false,"
        + " enabletooltip:true,"
        + "min:0,"
        + "inputType:1,"
        + "max:500,"
        + "limit:true"
        + '}"'
        + ':value="title" v-on:input="updatetitle"></tzinput>'
        + "</div>"
        + '<label class="stlabel">Title</label>'
        + '<div style="width:65%">'
        + '<tzinput :attribute="{'
        + "id: 'txtCagory',"
        + "isRequired:false,"
        + "enabletooltip:true,"
        + "min:0,"
        + "inputType:1,"
        + "max:500,"
        + "limit:true"
        + '}"'
        + ':value="category" v-on:input="updatecategory"></tzinput>'
        + "</div>"
        + '<label class="stlabel">Title Format</label>'
        + '<div style="width:65%">'
        + '<tzinput :attribute="{'
        + "id: 'txtTitleFormat',"
        + "isRequired:false,"
        + "enabletooltip:true,"
        + "min:0,"
        + "inputType:1,"
        + "max:500,"
        + "limit:true"
        + '}"'
        + ' :value="titleformat" v-on:input="updatetitleformat"></tzinput>'
        + "</div>"

        + '<div style="position:absolute; left:69%">'
        + '<button class="btn btn-primary" v-on:click="Save">Save</button>'
        + '<button class="btn btn-default" v-on:click="Close">Cancel</button>'
        + "</div>"
        + "</div>";
    var cdetail = {};
  //var rec=  Vue.compile(template);
    var myComp = function (appid,cmpid,next, fn) {
        var sthis = {};

        // this._data.title = "sampathkumar";
        $.ajax('/App/' + appid + '/Component/' + cmpid,
            {
                type: "GET",
                dataType: 'jsonp', // type of response data
                //  data: { clientid: id },
                // timeout milliseconds
                success: function (data, status, xhr) {   // success callback function    
                    cdetail = data;
                  
                    next(data);
                },
                error: function (jqXhr, textStatus, errorMessage) { // error callback
                    alert(errorMessage);
                }
            });
    }
    module.exports = {
        data: function () {
            return {
                titleformat: "",
                compName: "",
                category: "",
                title: "",
                key: "",
                 Details: function () { return cdetail} ,
            }
        },
        beforeCreate: function () {
           
            //this.set();
        },
        beforeRouteEnter(to, from, next) {
             
            myComp(from.params.id, from.params.cid, function (data) {

                next();
                //next(vm => {
                //    // access to component's instance using `vm` . this is done because this navigation guard is called before the component is created.
                //    vm.$data.Details = data;
                //   // vm.initializeSearch();
                 
                //}) 
            }) 
        },
        created: function () {
             
            this.set();
            this.AppID = this.$route.params.id;
            this.CompID = this.$route.params.action;           
        },
        mounted: function () {
           
            
        },
        //render: rec.render,
        //staticRenderFns: rec.staticRenderFns,
        methods: {
            set: function () {
              
                this.titleformat= this.Details().TitleFormat;
                this.compName = this.Details().ComponentName;
                this.category = this.Details().Category;
                this.title = this.Details().Title; 
            },
            updating: function (val) {
                this.compName = val;
            },
            updatetitle: function (val) {
                this.title = val;
            },
            updatetitleformat: function (val) {
                this.titleformat = val;
            },
            updatecategory: function (val) {
                this.category = val;
            },
            Save: function () {
                var othis = this;
                $.ajax('/App/' + this.AppID + '/Component/'+ this.CompID +'/update',
                    {
                        type: "POST",
                        dataType: 'jsonp', // type of response data
                        data: { "compName": this.compName, "title": this.title, "category": this.category, "titleformat": this.titleformat },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function
                            //that.Apps = data;this.$emit('increment');
                            othis.compName = "";
                            othis.title = "";
                            othis.compName = "";
                            othis.category = "";
                            othis.$emit('saved');
                            othis.Close();
                            //Window.toz.$refs.foo.push = [];
                            //Window.toz.$refs.foo.push = [];
                            Window.toz.$refs.foo.push({
                                header: "", body: "Component created successfully", template: ""
                                , autohide: true,
                                bodyCss: "feedback",
                                position: "top right",
                                autohide: true,
                            });
                            // that.Close();
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            Close: function () {
                this.$router.go(-1);
            },
            ChangeAction: function () {
             
            },
            forceRerender() {
                this.myComp();
            },
            myComp: function (next) {
                var sthis = {};
                sthis = this;                
                $.ajax('/App/' + this.AppID + '/Component/' + this.CompID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        //  data: { clientid: id },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function            
                          
                            next();
                            //form.title = data.Title;
                            //form.compName = data.ComponentName;
                            //form.titleformat = data.TitleFormat;
                            //form.category = data.Category;
                            //form.$nextTick(function () {
                            //    // DOM should be ready here...
                            //})
                      
                          

                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        },
                        form:this
                    });
            }
        },

    }
</script>
