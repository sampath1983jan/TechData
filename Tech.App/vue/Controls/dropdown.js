
define(function () {
    var dropdowntemplate = function (ele,attr) {
        var d = document.createElement("div");
        $(d).attr("data-toggle", "tooltip");
        $(d).attr("title", "this is required field");
        $(ele).find('select').appendTo(d);
       
        $(d).append('<small id="emailHelp" class="form-text help-text-muted">' + attr.note + '</small>');
        $(ele).append(d);
    }
    return Vue.component('tzdropdown',
        {
            template: '<div><select :id="id" class="form-control"  v-model="text" @change="handleInput">'                
                + '<option v-for="option in datasource" :value="option[valuefield]"  :key="option[valuefield]"> {{ option[displayfield] }}</option>'
                + ' </select ></div>'
            ,
            props: ['attribute'],
            data: function () {
                return {
                    required: true,
                    id:"select_" + this.attribute.id,
                    length: 200,
                    with: 200,
                    height: '',
                    text: this.value,
                    attr: this.attribute,
                    datasource: this.attribute.selectPicker.datasource,
                    valuefield: this.attribute.selectPicker.valueField,
                    displayfield: this.attribute.selectPicker.displayField,
                    type: this.attribute.selectPicker.type == undefined ? "select" : this.attribute.selectPicker.type,  // select,remote
                }
            },
            created: function () {

            },
            watch: {

            },
            mounted: function () {
                var othis = this;
              
                this.$nextTick(function () {
                    this.attr.inputType = 19;                 
                    dropdowntemplate(this.$parent.$el, this.attr);                  
                    $(othis.$parent.$el).find("select").Input(othis.attr);                                 
                });
            },
            updated: function () {              

            },
            destroyed: function () {
             
            },
            methods: {
                handleInput: function (e) {                  
                    this.$emit('input', this.text);
                },           
              
            },
            computed: {

            

            }
            // router: router
        });
});