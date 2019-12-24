<template>
    <div>
        <h2>Create New Form</h2>
        <div style='margin-left:35%'>
            <label class="stlabel">Form Name</label>
            <div style="width:65%">
                <tzinput :attribute="{id:'txtFormName',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="Name" v-on:input="updatename"></tzinput>

            </div>
            <label class="stlabel">Description</label>
            <div style="width:65%">
                <tzinput :attribute="{id:'txtDescription',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="Description" v-on:input="updatedesc"></tzinput>
            </div>


            <h4 class="stlabel">Create new Form from the scrach</h4>

            <div style="position:absolute; left:69%">
                <button class="btn btn-primary" v-on:click="Save">Save</button>
                <button class="btn btn-default" v-on:click="Close">Cancel</button>
            </div>
        </div>
    </div>
</template>
<script>
    module.exports = {
        name: "Form",
        components: {

        },
 
        data: function () {
            return {
                Name: "",
                Description:"",
            }
        }, beforeRouteEnter(to, from, next) {
            next();

        }, beforeRouteUpdate(to, from, next) {
            next();
        },
      
        created: function () {

        },
        methods: {
            updatename: function (val) {
                this.Name = val;
            },
            updatedesc: function (val) {
                this.Description = val;
            },
            Save: function () {
                var that = this;
                debugger;
                $.ajax('/App/' + appid + '/Form/CreateForm',
                    {
                        type: "POST",
                        dataType: 'jsonp', // type of response data
                        data: { "id": appid, "formname": this.Name, "description": this.Description },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function                           
                            that.Name = "";
                            that.Description = "";
                            that.$emit('saved');
                            that.Close();
                            Window.toz.$refs.foo.push({
                                header: "", body: "Form created successfully", template: ""
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
            Close: function () {

            }
        },       
    }
</script>
