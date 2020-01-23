<template>
    <!-- Name,description,short name, isactive -->
    <div>
        <div class="row">
            <div class="col-md-2">
                <label style="font-weight:bold; padding-top:5%">Label</label>
            </div>
            <div class="col-md-6">
                <tzinput :attribute="{id:'txtlabel',
            isRequired:false,
            min:0,
            inputType:1,
            max:500,
            limit:true,
                         placeholder:'Enter label'
            }" :value="Label" v-on:Input="changelabel"></tzinput>
            </div>


        </div>
        <div class="row">
            <div class="col-md-2">
                <label style="font-weight:bold; padding-top:5%">Short Label</label>
            </div>
            <div class="col-md-2">
                <tzinput :attribute="{id:'txtshortlabel',
            isRequired:false,
            min:0,
            inputType:1,
            max:500,
            limit:true,
                         placeholder:'Enter short label'
            }" :value="ShortLabel" v-on:Input="changeshortlabel"></tzinput>
            </div>
            <div class="col-md-1">
                <label style="font-weight:bold; padding-top:5%">Value</label>
            </div>
            <div class="col-md-2">
                <tzinput :attribute="{id:'txtvalue',
            isRequired:false,
            min:0,
            inputType:1,
            max:500,
            limit:true,
                         placeholder:'Enter Value'
            }" :value="Value" v-on:Input="changevalue"></tzinput>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <label style="font-weight:bold; padding-top:5%">Description</label>
            </div>
            <div class="col-md-6">
                <tztextarea :attribute="{id:'txtlkitemdescription',
            isRequired:false,
            min:0,
            inputType:1,
            max:500,
            limit:true,
            }" :value="Description" v-on:Input="changedescription"></tztextarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="text-align:right">
                <button class="btn btn-primary" value="Save" v-on:click="save">Save</button>
                <button class="btn btn-default" value="Back"  v-on:click="goBack">Back</button>
            </div>
        </div>
    </div>
</template>
<script>
    module.exports = {
        name: "addlookupitem",
        data: function(){
            return{
                Name: this.name,
                Label: "",
                ShortLabel: "",
                Value: "",
                Description:"",
            }
        },
        props:["name","lkid"],
        methods: {
            changelabel: function (val) {
                this.Label = val;
            },
            changeshortlabel: function (val) {
                this.ShortLabel = val;
            },
            changevalue: function (val) {
                this.Value = val;
            },
            changedescription: function (val) {
                this.Description = val;
            },
            save: function () {
                var that = this;
                 
                $.ajax('/App/' + appid + '/' + this.lkid +'/create/',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: { "label": this.Label, "description": this.Description,"value":this.Value, "shortname": this.ShortLabel },
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function
                            that.$emit("added");
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    }); 
             
            },
            goBack: function () {
                this.$emit("back");
            }
        }
    }
</script>