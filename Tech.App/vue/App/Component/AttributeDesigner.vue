<template>
    <div class=' '>
        <div class="row" style="border-bottom:0px solid #ddd; padding:0px;margin:0px; background-color:#f8f8f8">
            <div class="col-md-8"><h4 style="font-weight:bold;padding-top:9px">{{AttributeName}}</h4></div>
            <div class="col-md-4" style="text-align:right;top:5px"><a v-on:click="goback" style="cursor:pointer;"><i class="icofont-close-line icofont-2x"></i></a></div>
        </div>
        <!--Field name and title-->
        <div class='row' style="margin:25px 0px;">
            <div class="col-md-2">Field Name</div>
            <div class="col-md-9">
                <tzinput :attribute="{id:'txtAttributeName',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="AttributeName" v-on:input="attributeName"></tzinput>

            </div>
        </div>
        <div class='row' style="margin:25px 0px 0px;">
            <div class="col-md-2">Field Type</div>
            <div class="col-md-4">
                <dropdown :attribute="attFieldType" :value="AttributeType" v-on:input="fieldtype"></dropdown>
            </div>
            <div class="col-md-2">Length</div>
            <div class="col-md-3">
                <tzinput :attribute="{id:'txtLength',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:2,
            max:500,
            limit:false,format: 'hex',
            }" :value="Length" v-on:input="length"></tzinput>
            </div>
        </div>

        <div class='row' style="margin:25px 0px;">
            
        </div>
        <!--Field Defination-->
        <div class='row' style="margin:25px 0px;">
            <div class="col-md-12" style="padding-left:0px !important">
                <div class="row">
                    <div class="col-md-2">     Field defined as </div>
                    <div class="col-md-9">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12">Is Key</div>
                                    <div class="col-md-12">
                                        <tzinput :attribute="{id:'txtiskey',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:10,
            max:500,
            limit:false,format: 'hex',Note:'Set this field key field or not'
            }" :value="IsPrimary" v-on:input="iskey"></tzinput>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12">Is Null</div>
                                    <div class="col-md-12">
                                        <tzinput :attribute="{id:'txtisnull',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:10,
            max:500,
             Note:'allow null vlaue or not'
            }" :value="IsNull" v-on:input="isnull"></tzinput>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12">Is Core</div>
                                    <div class="col-md-12">
                                        <tzinput :attribute="{id:'txtiscore',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:10,
            max:500,
             Note:'Is Core field or not'
            }" :value="IsCore" v-on:input="iscore"></tzinput>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12">Is Req.</div>
                                    <div class="col-md-12">
                                        <tzinput :attribute="{id:'txtisreq',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:10,
            max:500,
             Note:'Is Required field or not'
            }" :value="IsRequired" v-on:input="isrequired"></tzinput>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12">Is Uniq.</div>
                                    <div class="col-md-12">
                                        <tzinput :attribute="{id:'txtisunique',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:10,
            max:500,
             Note:'Is Required field or not'
            }" :value="IsUnique" v-on:input="isunique"></tzinput>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>


            </div>
        </div>
        <!--Security-->
        <div class="row" style="margin:25px 0px;">
            <div class="col-md-2">
                Field Security
            </div>
            <div class="col-md-9">
                <div class="row">
                    <div class="col-md-12">Enable Field Encryption</div>
                    <div class="col-md-12">
                        <tzinput :attribute="{id:'txtisSecurity',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:10,
            max:500,
            limit:false,format: 'hex',Note:'Set this field key field or not'
            }" :value="IsSecured" v-on:input="issecurity"></tzinput>
                    </div>
                </div>
            </div>
        </div>
        <!--Other information-->
        <div class="row" style="margin:25px 0px;">
            <div class="col-md-2">Other information</div>
            <div class="col-md-9">
                <div class="row">
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-12">
                                LookUp
                            </div>
                            <div class="col-md-12">
                                <dropdown :attribute="attLookup" :value="LookUpID" v-on:input="lookupid"></dropdown>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                Default value
                            </div>
                            <div class="col-md-12">
                                <tzinput :attribute="{id:'txtdefault',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:2,
            max:500,
            limit:false,format: 'hex',Note:'set default value'
            }" :value="DefaultValue" v-on:input="defaultvalue"></tzinput>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                File Extenstions
                            </div>
                            <div class="col-md-12">
                                <tzinput :attribute="{id:'txtfileExtention',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:false,format: 'hex',Note:'set default value'
            }" :value="FileExtension" v-on:input="fileextention"></tzinput>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--Actions-->
        <div class="row">
            <div class="col-md-8">

            </div>
            <div class="col-md-4" style="margin-top:25px">
                <input type="button" class="btn btn-primary" v-on:click="Save" value="Save" />
                <input type="button" class="btn btn-default" v-on:click="Close" value="Cancel" />
            </div>
        </div>
    </div>

</template>
<script>
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

    module.exports = {
        components: {
            "popper": window.popper,
            'dropdown': window.dropdown_c,
        },
        data: function () {
            return {
                FieldID: "",
                AttributeName: "",
                FieldType: 0,
                Length: 0,
                IsNull: true,
                IsPrimary: false,
                TableID: "",
                IsRequired: false,
                IsUnique: false,
                IsCore: false,
                IsReadOnly: false,
                IsSecured: false,
                IsAuto: false,
                LookUpID: "",
                DefaultValue: "",
                FileExtension: "",
                RegExp: "",
                AttributeType: "",
                FieldTypeList: fieldtype,
                AttributeNameful:"",
                attFieldType: {
                    id: 'txtFieldType',
                    isRequired: false,
                    enabletooltip: true,
                    min: 0,
                    inputType: 19,
                    max: 500,
                    limit: true,
                    selectPicker: {
                        datasource: fieldtype,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose Attribute Type'
                    }
                },
                   attLookup: {
                    id: 'txtLookup',
                    isRequired: false,
                    enabletooltip: true,
                    min: 0,
                    inputType: 19,
                    max: 500,
                    limit: true,
                    selectPicker: {
                        datasource: fieldtype,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose Attribute Type'
                    }
                }
            }
        },
        beforeCreate: function () {
   
        },
        beforeRouteEnter(to, from, next) {
        
            next()             
        },
        created: function () {
          //  debugger;

          //  var avaa = this.$route.fullPath.split("/");
           
            this.FieldID = this.$route.params.attid;
            var appid = this.$route.params.id;
            var cid = this.$route.params.cid;
            if (this.FieldID == undefined) {
                this.FieldID = "";
                return;
            }          
            this.getAttribute(appid, cid, this.FieldID);
        },
        watch: {
            AttributeName: function () {                
         
            }
        },
        mounted: function () {      
            
        },         
        methods: {
            goback: function () {
                this.$router.go(-1);
            },
            length: function (val) { 
                this.Length = val;
            },
            fieldtype: function (val) {              
                this.FieldType = val;
                this.AttributeType = val;
            },
            attributeName: function (val) {
                this.AttributeName = val;
            },
            fileextention: function (val) {
                this.FileExtension = val;
            },
            defaultvalue: function (val) {
                this.DefaultValue = val;
            },
            lookupid: function (val) {
                this.LookUpID = val;
            },
 
            issecurity: function (val) {
               
                this.IsSecured = val;
            },
            isunique: function (val) {
                this.IsUnique = val;
            },
            isrequired: function (val) {
                this.IsRequired = val;
            },
            iscore: function (val) {
                this.IsCore = val;
            },
            isnull: function (val) {
                this.IsNull = val;
            },
            iskey: function (val) {
                this.IsPrimary = val;
            },
            Close: function () {
                this.$router.go(-1);
            },
            Save: function () {
                var that = this;
                var appid = this.$route.params.id;
                var cid = this.$route.params.cid;
   
                var attr = {};
                attr.FieldID= this.FieldID,
                    attr. AttributeName = this.AttributeName;
                attr.FieldType = this.AttributeType;
                attr.Length = this.Length;
                attr.IsNullable = this.IsNull;
                attr.IsPrimaryKey = this.IsPrimary;
                attr.TableID = this.TableID;
                attr.IsRequired = this.IsRequired;
                attr.IsUnique = this.IsUnique;
                attr.IsCore = this.IsCore;
                attr.IsReadOnly = false;
                attr.IsSecured = this.IsSecured;
                attr. IsAuto = false;
                attr.LookUpID = this.LookUpID;
                attr.DefaultValue = this.DefaultValue;
                attr.FileExtension = this.FileExtension;
                attr. RegExp = "";
                attr.AttributeType = this.AttributeType;
                $.ajax('/App/' + appid + '/Component/' + cid + "/Atrribute/Add",
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: { attribute: JSON.stringify(attr) },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function
                            that.attri = data;
                            that.$emit("onAdd");
                            that.$router.go(-1);
                            Window.toz.$refs.foo.push({
                                header: "", body: "Attribute created successfully", template: ""
                                , autohide: true,
                                bodyCss: "feedback",
                                position: "top right",
                                autohide: true,
                            });
                            //    console.log(that.ComponentInfo);
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            getAttribute: function (appid, id,aid) {
                var that = this             
                $.ajax('/App/' + appid + '/Component/' + id + "/change/" + aid,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        success: function (data, status, xhr) {   // success callback function
                             
                            that.FieldID = data.FieldID;
                            that.AttributeName = data.AttributeName;
                           // that.FieldType = data.FieldType;
                            that.Length = data.Length;
                            that.IsNull = data.IsNullable;
                            that.IsPrimary = data.IsPrimaryKey;
                            //that.TableID = data.TableID;
                            that.IsRequired = data.IsRequired;
                            that.IsUnique = data.IsUnique;
                            that.IsCore = data.IsCore;
                            that.IsReadOnly = data.IsReadOnly;
                            that.IsSecured = data.IsSecured;
                            that.IsAuto = data.IsAuto;
                            that.LookUpID = data.LookUpID;
                            that.DefaultValue = data.DefaultValue;
                            that.FileExtension = data.FileExtension;
                            that.RegExp = data.RegExp;
                            that.AttributeType = data.AttributeType;
                           
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
        },
    }

</script>