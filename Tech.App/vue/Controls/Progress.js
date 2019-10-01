
define(function () {   
    return Vue.component('tProgress',
        {
            template: '<div class="progress pink">'
                + '<h3 class="progress-title">{{title}}</h3><div class="progress-bar" v-bind:style="styleObject" style="background:#ff1170;">'
                + ' <div class="progress-value">{{text}}</div></div> </div>'
            ,
            props: ['title', "value","titleclass","barclass","valueclass"],
            data: function () {
                return {
                    styleObject: {
                        width: this.value + "%",  
                      
                    },
                    text: this.value,
                }
            },
            created: function () {

            },
            watch: {
                value: function (newVal, oldVal) { // watch it
                    this.text = newVal;
                    this.styleObject.width = newVal;
                    
                    $(this.$el).find(".progress-bar").css("width", newVal + "%");

                }
            },
            mounted: function () {
               
               
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
         
            
        });
});