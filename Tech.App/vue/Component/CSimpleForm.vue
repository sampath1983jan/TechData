<template>
    <div>
        <h2>Create New app</h2>
        <div style='margin-left:35%'>
            <label class="stlabel">App Name</label>
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
            <label>Description</label>
            <div style="width:65%">
                <tztextarea v-model="description" :attribute="{id:'txtDescription',
        isRequired:false,
        enabletooltip:true,
        min:0,
        inputType:2,
        max:511,
        limit:true
        }"></tztextarea>
            </div>
        
            
                <h4 class="stlabel">Create new app from the scrach</h4>
           
            <div  style="position:absolute; left:69%">
                <button class="btn btn-primary" v-on:click="Save">Save</button>
                <button class="btn btn-default" v-on:click="Close">Cancel</button>
            </div>
        </div>
        </div>
</template>


<script>
     module.exports = {
         data: function (){
             return {
                 Apps: [],
                 compName: "",
                 description:"",
             }
         },
         created: function () {

         },

        methods: {
            updating: function (val) {
                this.appName = val;
    },
            Close: function () {
                 jPopup.prototype.close(true);
             },
             Save:function() {
                 var that = this;
                 $.ajax('/App/Save',
                     {
                         type: "POST",
                         dataType: 'jsonp', // type of response data
                         data: { "Name": this.$data.appName, "description": this.$data.description },
                         // timeout milliseconds
                         success: function (data, status, xhr) {   // success callback function
                             //that.Apps = data;this.$emit('increment');
                             that.appName = "";
                             that.$emit('saved');
                             that.Close();
                             //Window.toz.$refs.foo.push = [];
                             //Window.toz.$refs.foo.push = [];
                             Window.toz.$refs.foo.push({
                                 header: "", body: "App Created Successfully", template:""
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
             getApps: function () {
                 var that = this;
                 $.ajax('/App/Get',
                     {
                         type: "GET",
                         dataType: 'jsonp', // type of response data
                         data: {  },
                         // timeout milliseconds
                         success: function (data, status, xhr) {   // success callback function
                             that.Apps = data;
                         },
                         error: function (jqXhr, textStatus, errorMessage) { // error callback
                             alert(errorMessage);
                         }
                     });
             }
         },
    }


</script>