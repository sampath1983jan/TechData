<template>
    <div>
        <h2>Create New Component</h2>
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
        
            
                <h4 class="stlabel">Create new component from the scrach</h4>
           
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
                 Components: [],
                 compName: "",
                 title:"",
             }
         },
         created: function () {

         },

        methods: {
            updating: function (val) {
                this.compName = val;
            }, updatetitle: function (val) {
                this.title = val;
            },
            Close: function () {
                 jPopup.prototype.close(true);
             },
             Save:function() {
                 var that = this;
                 $.ajax('/App/'+ appid +'/Component/Create',
                     {
                         type: "POST",
                         dataType: 'jsonp', // type of response data
                         data: {"id":appid, "compName": this.$data.compName, "title": this.$data.title },
                         // timeout milliseconds
                         success: function (data, status, xhr) {   // success callback function
                             //that.Apps = data;this.$emit('increment');
                             that.compName = "";
                             that.title = "";
                             that.$emit('saved');
                             that.Close();
                             //Window.toz.$refs.foo.push = [];
                             //Window.toz.$refs.foo.push = [];
                             Window.toz.$refs.foo.push({
                                 header: "", body: "Component created successfully", template:""
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