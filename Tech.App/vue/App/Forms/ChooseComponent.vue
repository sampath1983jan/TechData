<template>
    <div>
        <div class="row" style="margin-left:20% !important">
            <div class="col-md-8" >
                <div>Choose Component to build Form</div>
                <div><dropdown :attribute="attribute" v-on:input="update"></dropdown></div>
            </div>
        </div>
        
        <div class="row" style="margin-left:20%  !important">
            <div class="col-md-8" style="text-align:right; margin-top:15px;">
                <input type="button" class="btn btn-primary" value="Save" v-on:click="save" />
                <input type="button" class="btn btn-default" value="Cancel" v-on:click="cancel" />
            </div>
            
        </div>
    </div>
</template>
<script>
    module.exports = {
        name: "ChooseComponent",
        components: {
            "popper": window.popper,
            'dropdown': window.dropdown_c,
        },
        data: function () {
            return {
                FormID: this.formid,
                AppID: this.appid,    
                Comps: [],
                Component:"",
                attribute: {
                    id: 'ddComponent',
                    isRequired: false,
                    enabletooltip: true,
                    min: 0,
                    inputType: 19,
                    max: 500,
                    limit: true,
                    selectPicker: {
                        datasource: this.comp,
                        displayField: 'ComponentName',
                        valueField: 'ComponentID',
                        selection: 'single',
                        defaultSelection: 'Choose Component'
                    }
                }
            }
        },        
        props: ["appid","comp","formid"],
        created: function () {
        //    this.getComp();
        },
        methods: {
            update: function (val) {
                this.Component = val;
            },
            cancel: function(){

            }, 
                save: function() {
                if (this.Component == "") {
                    alert("Choose Component");
                    return false;
                }
                $.ajax('/App/' + this.AppID + '/Form/AssignComponent/' + this.FormID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: { compID: this.Component},
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function     
                            var d = [];
                            $.each(data, function (i, v) {
                                d.push(v.Component);
                            })
                            that.Comps = data;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            getComp: function () {
                var that = this;
              
                $.ajax('/MyApp/' + this.AppID + '/Components',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: {},
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function     
                            var d = [];
                            $.each(data,function (i,v) {
                                d.push(v.Component);
                            }) 
                            that.Comps = data;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });

            }
      
        },
        //router: router
    }
</script>