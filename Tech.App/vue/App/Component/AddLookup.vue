<template>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-2">
                    <label>Lookup Name</label>
                </div>
                <div class="col-md-6">
                    <tzinput :attribute="{id:'txtLookupName',
            isRequired:false,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="Name" v-on:Input="changename"></tzinput>
                </div>
            </div>
            <div class="row">

                <div class="col-md-2">
                    <label>Description</label>
                </div>
                <div class="col-md-6">
                    <tztextarea :attribute="{id:'txtlkDescription',
            isRequired:false,
            min:0,
            inputType:1,
            max:500,
            limit:true,
            }" :value="Description" v-on:Input="changedescription"></tztextarea>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <label>Is Core</label>
                </div>
                <div class="col-md-3">
                    <tzinput  :attribute="attCore" v-on:Input="changecore"></tzinput>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align:right">
                    <button class="btn btn-primary" value="Save" v-on:click="save">Save</button>
                    <button class="btn btn-default" value="Back" v-on:click="goback">Back</button>
                </div>
            </div>
        </div>
     
    </div>
</template>
<script>
    module.exports = {
        name: "AddLookup",
        components: {},
        data: function () {
            return {
                Name: "",
                Description: "",
                IsCore: false,
                attCore: {
                    id: 'ckCore',
                    isRequired: false,
                    enabletooltip: true,
                    inputType: 10,
                    Note: ''
                }
            }
        },
        created: function () {
             
        },
        methods: {
            changecore: function (val) {
                this.IsCore = val;
            },
            changename: function (val) {
                this.Name = val;
            },
            changedescription: function (val) {
                this.Description = val;
            },
            save: function () {
                $.ajax('/App/' + appid + '/Lookup/Create/',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: { "lookupname": this.Name, "description": this.Description,"iscore":this.IsCore },
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function
                            that.$emit("Added");
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    }); 
            },
            goback: function () {
              
                this.$emit("cancelled");
            }
        }
    }
</script>

