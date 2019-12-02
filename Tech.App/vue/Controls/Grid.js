define(function () {
    
    return Vue.component('tzGrid',
        {
            template: '<div><table :id="idm" style="width:100%"></table></div>'            ,
            props: ['columns', "data", "pagging","callback", 'id'],
            data: function () {
                return {
                    Cols: this.columns,
                    datasource: this.data,
                    idm: this.id,
                    page: this.pagging,
                    ajaxCallback: this.callback
                };
            },
            created: function () {

            },
            watch: {

            },
            mounted: function () {
                var othis = this;
         
                this.$nextTick(function () {                    
                    $(this.$parent.$el).find("table").DataGrid({
                        columns: othis.Cols,
                        data: othis.datasource,
                        callback: othis.ajaxCallback,
                        pagging: {
                            pageSize: this.page.pagesize
                        }
                    });
                });
            },
            updated: function () {
             

            },
            destroyed: function () {
               
            },
            methods: {
                 
            },
            computed: {
                fullName: {
                    // getter
                    get: function () {
                        return this.firstName + ' ' + this.lastName;
                    },
                    // setter
                    set: function (newValue) {
                        var names = newValue.split(' ');
                        this.firstName = names[0];
                        this.lastName = names[names.length - 1];
                    }
                },
                getValue: {
                    // getter
                    get: function () {

                        if (this._props.attribute.inputType == 5) {
                            return this.gettime();
                        } else {
                            return this.text;
                        }
                        //  return this.text;
                    },
                    // setter
                    set: function (newValue) {
                        this.text = newValue
                    }
                },
                getType: {
                    get: function () {
                        var txt = "text";
                        if (this.attribute.inputType == 1) { //textbox
                            txt = "text";
                        } else if (this.attribute.inputType == 2) { //password
                            txt = "text";
                        }
                        if (this.attribute.inputType == 10 || this.attribute.inputType == 16) { // checkbox
                            txt = "checkbox";
                        } else if (this.attribute.inputType == 11) { // radio
                            txt = "radio";
                        } else if (this.attribute.inputType == 12) {
                            txt = "file";
                        }
                        return txt;
                    },
                    set: function () {

                    }

                },
                getTemplate: {
                    get: function () {
                        if (this.attribute.inputType == 8) {
                            return '<textarea rows="5"  v-model="text" id="" class="form-control" placeholder=""/>';
                        } else {
                            return '';
                        }

                    },
                    set: function () { }
                }
            }
            // router: router
        });
});
