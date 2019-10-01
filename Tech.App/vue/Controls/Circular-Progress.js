
//https://codepen.io/sergiopedercini/pen/jmKdbj
define(function () {
    return Vue.component('cProgress',
        {
            template: '<div class="single-chart">'
            +'<svg viewBox="0 0 36 36" :width="width" :height="height" class="circular-chart">'
            +'<path class="circle-bg"'
            +'d="M18 2.0845 a 15.9155 15.9155 0 0 1 0 31.831 a 15.9155 15.9155 0 0 1 0 -31.831"  />'
                +'<path class="circle" :stroke="bar" stroke-dasharray="0, 100" d="M18 2.0845 a 15.9155 15.9155 0 0 1 0 31.831 a 15.9155 15.9155 0 0 1 0 -31.831"/>'
    +'<text x="18" y="20.35" class="percentage">{{text}}%</text>'
        + '</svg>'
        + '</div>',
            props: ["value","width","height","barcss"],
            data: function () {
                return {
                    text: this.value,
                    bar:"#17a2b8" 
                }
            },
            created: function () {

            },
            watch: {
                value: function (newVal, oldVal) { // watch it
                    this.text = newVal;
                    if (this.text == "") { this.text = 0;}
                    var inval = 0;
                    var othis = this;
                    var showPercent = window.setInterval(function () {                      
                       
                        $(othis.$el).find(".circle").attr("stroke-dasharray", inval + ",100");
                        inval = inval + 1;
                        if (inval > newVal) {
                            clearInterval(showPercent);
                        }
                    }, 1); // Runs at a rate based on the animation's

                  
                    
                }
            },
            mounted: function () {
                this.$nextTick(function () {
                    $(this.$el).find(".circle").attr("stroke-dasharray", this.text + ",100");
                //    $(this.$el).find(".circle").attr("stroke", this.bar);
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


        });
});