<template>
    <div>
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                    Component Name
                </div>
                <div class="col-md-3">
                    Title
                </div>
                <div class="col-md-3">
                    Category
                </div>
                <div class="col-md-2">
                    Component Type
                </div>
            </div>
            <div class="row"  v-for="d in Comps">
                <div>
                    <input type="radio" v-model="d.checked" name="rdSelectComp" v-on:click="choose(d)" />
                </div>
                <div class="col-md-3">
                    {{d.Component.ComponentName}}
                </div>
                <div class="col-md-3">
                    {{d.Component.Title}}
                </div>
                <div class="col-md-3">
                    {{d.Component.Category}}
                </div>
                <div class="col-md-2">
                    {{d.Component.ComponentType}}
                </div>
            </div>
        </div>
        <div>
            <button class="btn btn-primary" value="Choose" v-on:click="onselected">Choose</button>
            <button class="btn btn-default" value="Back" v-on:click="goBack">Back</button>
        </div>
    </div>
</template>

<script>


    module.exports = {
        data: function () {
            return {
                Appid:"",
                Comps: [],
                renderComponent: this.refreshList,
                selected: {},
            }
        },
        props: ["appid","refreshList"],
        created: function () {           
            this.Appid = appid;
            this.getComp();
        },
        methods: {
            choose: function (eid) {
                this.selected = eid;           
              
            },
            onselected: function () {
              
                 
                this.$emit("selected", this.selected.Component.ComponentID, this.selected.Component.ComponentName);
            },

            getAttributes: function () {
                var that = this;
                $.ajax('/App/' + appid + '/Component/' + that.selected.Component.ComponentID,
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
            forceRerender() {
                this.getComp();
            },
            goBack: function () {
                this.$emit("back");
            },       
            getComp: function () {
                var that = this;
                 
                $.ajax('/MyApp/' + appid + '/Components',
                        {
                            type: "GET",
                            dataType: 'jsonp', // type of response data
                            data: {},
                            // timeout milliseconds
                            success: function (data, status, xhr) {  // success callback function
                                var d = [];
                                 
                                $.each(data, function (i,v) {
                                    v.checked = false;
                                })
                                that.Comps = data;
                            },
                            error: function (jqXhr, textStatus, errorMessage) { // error callback
                                alert(errorMessage);
                            }
                        });
                

            }
        },

    }
</script>
