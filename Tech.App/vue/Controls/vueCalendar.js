define(function () { 
    return Vue.component('Calendar',
        {
            template: '<div :id="attr.id"></div>'
            ,
            props: ['attr'],
            data: function () {
                return {
                    attributes: this.attr,
                }
            },
            created: function () {

            },
            watch: {

            },
            mounted: function () {
                var othis = this;
                this.$nextTick(function () {                   
                    $("#" + this.attr.id).Calendar(this.attr).init();
                  
                });
            },
            updated: function () {

            },
            destroyed: function () {

            },
            methods: {
              

            },
            computed: {



            }
            // router: router
        });
});