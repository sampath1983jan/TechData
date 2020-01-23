<template>
    <div class="container-fluid">
        <div class="row" v-if="IsViewLookUp==false">
            <div class="col-md-12" style="padding:0">
                <div class="row" style="margin:10px 0px;">
                    <div class="col-md-12"  style="padding:0">
                        <h4 style="font-weight:bold">Basic Field Information</h4>
                        <div class="row" style="margin:10px 0px;">
                            <div class="col-md-12" style="padding:0">
                                <div class="container">
                                    <div class="row" style="margin:10px 0px;">
                                        <div class="col-12 col-sm-6 col-lg-12">Display Name</div>
                                        <div class=" co-12 col-sm-6 col-lg-12">
                                            {{AttributeName}}
                                        </div>
                                    </div>
                                    <div class="row" style="margin:10px 0px;">
                                        <div class="col-sm-12 col-md-2 ">Render Type</div>
                                        <div class="col-sm-12 col-md-6">
                                            <dropdown :attribute="renderType" :value="Category" v-on:input="changeRender"></dropdown>
                                        </div>
                                    </div>
                                    <div class="row" style="margin:10px 0px;" v-if="IsEnableRequired">
                                        <div class="col-md-2 col-xl-12">Enable Limit</div>
                                        <div class="col-md-6 col-xl-12">
                                            <tzinput :disable="IsdisableLimit" :attribute="attEnable" :value="EnableLimit" v-on:Input="changeLimit"></tzinput>
                                        </div>
                                    </div>
                                    <div class="row" v-if="EnableLimit" style="margin:10px 0px;">
                                        <div class="col-md-2">
                                            Limit
                                        </div>
                                        <div class="col-md-3">
                                            <tzinput :attribute="{id:'txtmax',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:2,
            max:500,
            limit:true
            }" :value="Min" v-on:Input="changeMin"></tzinput>
                                        </div>
                                        <div class="col-md-1" style="text-align:center;margin-top:10px">to</div>
                                        <div class="col-md-3">
                                            <tzinput :attribute="{id:'txtmin',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:2,
            max:500,
            limit:true
            }" :value="Max" v-on:Input="changeMax"></tzinput>
                                        </div>
                                    </div>
                                    <div class="row" style="margin:10px 0px;">

                                        <div class="col-md-2" v-if="IsDecimal">
                                            <label> No of Decimal place</label>
                                        </div>
                                        <div class="col-md-4" v-if="IsDecimal">
                                            <tzinput :attribute="{id:'txtdecimalplace',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="DecimalPlace" v-on:Input="decimalplacechange"></tzinput>
                                        </div>
                                        <div class="col-md-2" v-if="IsCurrency">
                                            <label>Choose Currency</label>
                                        </div>
                                        <div class="col-md-4" v-if="IsCurrency">
                                            <dropdown :attribute="currency" v-on:input="changecurrency"></dropdown>
                                        </div>
                                    </div>
                                    <div class="row" v-if="IsLookup">
                                        <div class="col-md-2">
                                            Choose Lookup source
                                        </div>
                                        <div class="col-md-2" v-if="LookupID==''">
                                            <input type="button" class="btn btn-primary" value="AddLookup" v-on:click="ShowLookup" />
                                        </div>
                                        <div class="col-md-8" v-if="LookupID!=''">
                                            <div class="row">
                                                <div class="col-md-5" style="overflow:hidden">
                                                    {{LookupName}}
                                                    <a v-on:click="ShowLookup" style="cursor:pointer;color:palevioletred">Change Lookup </a>
                                                </div>
                                                <div class="col-md-2">Display Field</div>
                                                <div class="col-md-3">
                                                    <dropdown :attribute="lookupdisplay" :value="LookUpDisplayField" v-on:input="changedisplayfield"></dropdown>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">Value Field</div>
                                                <div class="col-md-3">
                                                    <dropdown :attribute="lookupvalue" :value="LookUpValueField" v-on:input="changevaluefield"></dropdown>
                                                </div>
                                                <div class="col-md-2">Sort by</div>
                                                <div class="col-md-3"> </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin:10px 0px;" v-if="IsText">
                                        <div class="col-md-2">
                                            Format
                                        </div>
                                        <div class="col-md-6">
                                            <tzinput :attribute="{id:'txtFormat',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }"></tzinput>
                                        </div>
                                    </div>
                                    <div class="row" style="margin:10px 0px;" v-if="IsDate==true || IsTime==true">
                                        <div class="col-md-12"><label style="font-weight:bold">Date & Time Fomat</label></div>
                                        <div class="col-md-12">
                                            <div class="row" style="margin:10px 0px;">
                                                <div class="col-md-2">Date Fomat</div>
                                                <div class="col-md-3">
                                                    <dropdown :attribute="dateAttribute" :value="DateFormat" v-on:Input="changedateformat"></dropdown>
                                                </div>
                                            </div>
                                            <!--<div class="row">
                    <div class="col-md-2">Date & Time Fomat</div>
                    <div class="col-md-3">
                        <dropdown :attribute="attFieldType" :value="AttributeType" v-on:input="fieldtype"></dropdown>
                    </div>
                    <div class="col-md-2">Picker Interval</div>
                    <div class="col-md-3">
                        <dropdown :attribute="attFieldType" :value="AttributeType" v-on:input="fieldtype"></dropdown>
                    </div>
                </div>-->
                                            <div class="row" style="margin:10px 0px;" v-if="IsTime">
                                                <div class="col-md-2">Time Fomat</div>
                                                <div class="col-md-3">
                                                    <dropdown :attribute="timeAttribute" :value="TimeFormat" v-on:Input="changetimeformat"></dropdown>
                                                </div>
                                                <div class="col-md-2">Picker Interval</div>
                                                <div class="col-md-3">
                                                    <dropdown :attribute="timeInterval" :value="PickerInterval" v-on:Input="changepickerinterval"></dropdown>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" v-if="IsFile">
                                        <div class="col-md-2">
                                            <label>Max File size <small style="float:right" class="form-text help-text-muted">(in bytes)</small></label>

                                        </div>
                                        <div class="col-md-4">
                                            <tzinput :attribute="{id:'txtMaxFileSize',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="MaxFileSize" v-on:Input="changemaxfilesize"></tzinput>
                                        </div>
                                        <div class="col-md-2">
                                            <label>File Extension </label>
                                            <small class="form-text help-text-muted">(Ex:txt,pdf,doc,xls,xlsx,png,gif)</small>
                                        </div>
                                        <div class="col-md-4">
                                            <tzinput :attribute="{id:'txtfilelist',
            isRequired:false,
            enabletooltip:true,
            min:0,
            inputType:1,
            max:500,
            limit:true
            }" :value="FileExtension" v-on:Input="changefileextension"></tzinput>
                                        </div>
                                    </div>
                                    <div class="row" v-if="IsDate==true || IsTime==true" style="margin:10px 0px;">
                                        <div class="col-md-12">
                                            <label style="font-weight:bold">Restriction</label>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="row" v-if="IsTime" style="margin:10px 0px;">
                                                <div class="col-md-2">Allowed Hours</div>
                                                <div class="col-md-4">
                                                    <dropdown :attribute="allowFrom" :value="AllowedFromHours" v-on:input="changefromhr"></dropdown>
                                                </div>
                                                <div class="col-md-1" style="text-align: center; margin-top: 10px;">to</div>
                                                <div class="col-md-4">
                                                    <dropdown :attribute="allowToh" :value="AllowedToHours" v-on:input="changetohr"></dropdown>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">Allowed days to choose</div>
                                                <div class="col-md-6">
                                                    <dropdown :attribute="allowDays" :value="AllowedDays" v-on:input="changeDays"></dropdown>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                            </div>


                    </div>
                </div>
                <div>
                    <div class="row">
                        <div class="col-md-8">

                        </div>
                        <div class="col-md-4" style="margin-top:25px">
                            <input type="button" class="btn btn-primary" v-on:click="Save" value="Save" />
                            <input type="button" class="btn btn-default" v-on:click="Close" value="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="" v-show="Show">
            <div class="col-md-12" id="lkform">
              
            </div>
        </div>

    </div>  
</template>
<script>
  
    var cmlok = httpVueLoader('/vue/App/Component/LookUp.vue?' + (Math.random() * 10000));
    var lookupfield = [{ "key": "label", "value": "Label" }, { "key": "shortlabel", "value": "Short Label" }];
    module.exports = {
        components: {
            "popper": window.popper,
            'dropdown': window.dropdown_c,
        },
        props: ["attribute", "formid", "attributename", "fieldid", "rendertype","formfieldattribute"],
        data: function () {
            return {
                AttributeName: this.attributename,
                Min: 0,
                Max: 0,
                DecimalPlace: 2,
                FieldID: this.fieldid,
                IsText:false,
                IsDate: false,
                IsTime: false,
                IsFile: false,
                IsDecimal: false,
                IsCurrency:false,
                EnableLimit: false,
                IsdisableLimit: false,
                IsEnableRequired: true,
                IsLookup: false,
                IsViewLookUp: false,
                dateAttribute: {
                    id: 'dtformat',                     
                    inputType: 19,
                    max: 500,
                    limit: true,
                    selectPicker: {
                        datasource: dateformat,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose Date Format'
                    }
                },
                timeAttribute: {
                    id: 'timefrm',
                    inputType: 19,
                    max: 500,
                    limit: true,
                    selectPicker: {
                        datasource: timeformat,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose Time Format'
                    }
                },
                timeInterval: {
                    id: 'timeinternval',
                    inputType: 19,
                    max: 500,                     
                    selectPicker: {
                        datasource: timeinternval,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose Time internval'
                    }
                },
                renderType: {
                    id: 'renderType',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: this.getRenderTypeSource(),
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose Render Type'
                    }
                },
                allowDays: {
                    id: 'allowdays',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: days,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'multi',
                        defaultSelection: 'Choose Days'
                    }
                },
                allowFrom: {
                    id: 'allowfrom',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: hours,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose From Hours'
                    }
                },
                allowToh: {
                    id: 'allowto',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: hours,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose To Hours'
                    }
                },
                currency: {
                    id: 'currency',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: currency,
                        displayField: 'name',
                        valueField: 'code',
                        selection: 'single',
                        defaultSelection: 'Choose Currency'
                    }
                },
                attEnable: {
                    id: 'txtLimit',
                    isRequired: false,
                    enabletooltip: true,
                    inputType: 10,
                    Note: ''
                },
                LookupID: "",
                LookUpDisplayField: "",
                LookUpValueField:"",
                AllowOrderbyAlphabet:false,
                lookupdisplay: {
                    id: 'lkdisplay',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: lookupfield,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose To lookup field'
                    }
                },
                lookupvalue: {
                    id: 'lkvalue',
                    inputType: 19,
                    max: 500,
                    selectPicker: {
                        datasource: lookupfield,
                        displayField: 'value',
                        valueField: 'key',
                        selection: 'single',
                        defaultSelection: 'Choose To lookup field'
                    }
                },
                FormID: this.formid,
                DateFormat: "",
                TimeFormat: "",
                PickerInterval: "0",
                MaxFileSize: 5000,
                FileExtension: "txt,doc",
                AllowedFromHours: "",
                AllowedToHours: "",
                AllowedDays:"",
                Category: "",
            }
        },

        created: function () {
            this.Category = this.rendertype;
            this.changeRender(this.Category);            
            this.Min = this.formfieldattribute.Min;
            this.Max = this.formfieldattribute.Max;
            this.DateFormat = this.formfieldattribute.DateFormat;
            this.AllowedDays = this.formfieldattribute.AllowedDays;
            debugger;
            this.AllowedFromHours = this.formfieldattribute.AllowedFromHours;
            this.AllowedToHours = this.formfieldattribute.AllowedToHours;
            this.LookUpDisplayField = this.formfieldattribute.TimeFormat;
            this.DecimalPlace = this.formfieldattribute.DecimalPlace;
            this.TimeFormat = this.formfieldattribute.TimeFormat;
            this.PickerInterval = this.formfieldattribute.pickerInterval;
            this.MaxFileSize = this.formfieldattribute.MaxFileSize;
            this.FileExtension = this.formfieldattribute.FileExtension;

            this.EnableLimit = this.formfieldattribute.Attribute.EnableLimit;
         
        },
        mounted: function () {
            
        },
        computed: {
            Show: function () {
                
                if (this.IsViewLookUp) {
                    this.Render();
                } else {
                    $("#lkform").html("");
                    return false;
                }
                return this.IsViewLookUp
            }
        },
        methods: {
            getRenderTypeSource: function () {
                var a = this.getRenderType();
            return    renderType.filter(function (x) {
                    return x.basetype == a;
                });
            },
            getRenderType: function () {
                if (this.attribute.AttributeType == 0 || this.attribute.AttributeType == 1 || this.attribute.AttributeType == 4) {
                    return 5
                } else if (this.attribute.AttributeType == 7 || this.attribute.AttributeType == 8) {
                    return 3;
                } else if (this.attribute.AttributeType == 5 || this.attribute.AttributeType == 6) {
                    return 1
                } else if (this.attribute.AttributeType == 9 || this.attribute.AttributeType == 10 || this.attribute.AttributeType == 11) {
                    return 2
                } else if (this.attribute.AttributeType == 12 ) {
                    return 4
                } else if (this.attribute.AttributeType == 2 || this.attribute.AttributeType == 3) {
                    return 0
                }
            },
            ShowLookup: function () {              
                this.IsViewLookUp = true;           
            },
            getback: function () {                
                that.IsViewLookUp = false;
            },
            onselection: function () {

            },
            changedisplayfield: function (val) {
                this.LookUpDisplayField = val;
            },
            changevaluefield: function (val) {
                this.LookUpValueField = val;
            },
            Render: function () {
                var that = this;
                $("#lkform").append("<div><look-up v-on:back='getback' v-on:selected='onselection'></look-up></div>");
                var mydat = new Vue({
                    components: {
                        'look-up': cmlok
                    },
                    methods: {
                        getback: function () {                            
                            that.IsViewLookUp = false;
                        },
                        onselection: function (val, name) {
                           
                            that.LookupID = val;
                            that.LookupName = name;
                            that.IsViewLookUp = false;
                        }
                    }
                }).$mount('#lkform');
            },
            Save: function () {
                var fld = {};
                fld.FormID = this.FormID;
                fld.FormFieldID = "";
                fld.EnableLimit = this.EnableLimit;
                fld.RenderType = this.getRenderType();
                fld.Category = this.Category;
                fld.DecimalPlace = this.DecimalPlace;
                fld.LookUpSource = this.LookupID;
                fld.DisplayField = this.LookUpDisplayField;
                fld.AllowOrderbyAlphabet = this.AllowOrderbyAlphabet;
                fld.DateFormat = this.DateFormat;
                fld.TimeFormat = this.TimeFormat;
                fld.pickerInterval = this.PickerInterval;
                fld.MaxFileSize = this.MaxFileSize;
                fld.FileExtension = this.FileExtension;
                fld.AllowedFromHours = this.AllowedFromHours;
                fld.AllowedToHours = this.AllowedToHours;
                fld.AllowedDays = this.AllowedDays;
                fld.Min = this.Min;
                fld.Max = this.Max;
                fld.FieldID = this.FieldID;
                this.$emit("save", fld);
            },
            Close: function () {
                 
                this.$emit("back");
            },
            changecurrency: function (val) {

            },
            changefromhr: function (val) {
                this.AllowedFromHours = val;

            },
            changetohr: function (val) {
                this.AllowedToHours = val;
            },
            changeDays: function (val) {
                this.AllowedDays = val;
            },
            changeLimit: function (val) {
                this.EnableLimit = val;
            },
            changeRender: function (val) {       
                this.Category = val;
                if (val >= 20 && val <= 22) {
                    this.IsDate = true;
                } else {
                    this.IsDate = false;
                }              
                if (val == 5 || val ==13) {                   
                    this.IsdisableLimit = true;
                    this.EnableLimit = true;
                } else {                    
                    this.IsdisableLimit = false;
                    // this.EnableLimit = false;
                }                
                if (val == 1
                    || val == 0) {
                    this.IsText = true;
                } else {
                    this.IsText = false;
                }
                if (val == 3) {
                    this.IsDecimal = true;
                } else
                    this.IsDecimal = false;
                if (val == 4) {
                    this.IsCurrency = true
                    this.IsDecimal = true;
                } else {
                    this.IsCurrency = false;
                }
                if (val >= 7 && val <= 11) {
                    this.IsLookup = true;
                } else {
                    this.IsLookup = false;
                }
                if (val >= 16 && val <= 19) {
                    this.IsFile = true;
                } else
                    this.IsFile = false;
                if (val == 21 || val == 23 || val == 24) {
                    this.IsTime = true;
                } else
                    this.IsTime = false;

                if ((val >= 7 && val <= 11) || val == 12 || val == 14 || val == 15 || (val >=16 && val<=19) || val==23 || val==24 || val==26 || val ==27) {
                    this.IsEnableRequired = false;
                    this.EnableLimit = false;
                } else {
                    this.IsEnableRequired = true;
                }
            },
            changeMin: function (val) { this.Min = val; },
            changeMax: function (val) { this.Max = val; },
            decimalplacechange: function (val) { this.DecimalPlace = val; },
            changedateformat: function (val) { this.DateFormat = val; },
            changetimeformat: function (val) { this.TimeFormat = val; },
            changepickerinterval: function (val) { this.PickerInterval = val; },
            changemaxfilesize: function (val) { this.MaxFileSize = val; },
            changefileextension: function (val) { this.FileExtension = val; },
        }


    }

</script>