
define(function () {
    var dropdowntemplate = function (ele, attr) {
        var d = document.createElement("div");
        $(d).attr("data-toggle", "tooltip");
        $(d).attr("title", "this is required field");
        $(ele).find('select').appendTo(d);

        $(d).append('<small id="emailHelp" class="form-text help-text-muted">' + attr.note + '</small>');
        $(ele).append(d);
    }
    return Vue.component('tzdropdown',
        {
            template: '<div><select :id="id" class="form-control"  multiple @click="handleInput"  @change="handleInput">'
                + '<option v-for="option in datasource" :value="option[valuefield]"  :key="option[valuefield]"> {{ option[displayfield] }}</option>'
                + ' </select ></div>'
            ,
            props: ['attribute',"selectedVal"],
            data: function () {
                return {
                    required: true,
                    id: "aufill_" + this.attribute.id,
                    length: 200,
                    with: 200,
                    height: '',
                    text: this.selectedVal,
                    attr: this.attribute,
                    datasource: this.attribute.autofill.datasource,
                    valuefield: this.attribute.autofill.valueField,
                    displayfield: this.attribute.autofill.displayField
                }
            },
            created: function () {

            },
            watch: {

            },
            mounted: function () {
                var othis = this;              
                this.$nextTick(function () {
                    this.attr.inputType = 17;
                    dropdowntemplate(this.$parent.$el, this.attr);
                    $(this.$parent.$el).find("select").Input(this.attr);                    
                    $(this.$parent.$el).find("select").on('tokenize:tokens:added', function (e, value) {
                        othis.text = value;                      
                        othis.text= $(this).tokenize2().toArray();
                        othis.$emit('click', othis.text);
                    });                                
                });
            },
            updated: function () {           

            },
            destroyed: function () {             
            },
            methods: {
                handleInput: function (e) {                                   
                   
                },


            },
            computed: {



            }
            // router: router
        });
});