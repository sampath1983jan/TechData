
//https://codepen.io/alexgibson/pen/MbpxKo

define(function () {
    return Vue.component('progressmeter',
        {
            template: '<div class="inlined progress-meter">'
                + ' <div class="track"> <span class="progressing"></span></div>'
                + '  <ol class="progress-points">'
                + ' <li class="progress-point" v-for="item in getPoints" :style="item"> <span class="label"> {{item.label}}</span></li>'
                +'</ol>  </div>'
            ,
            props: ["points","valuepoint"],
            data: function () {
                return {                   
                    currentPoint: -1,
                    dataPoint: this.points,
                    startPoint: this.valuepoint
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
              
                this.$nextTick(function () {
                    var othis = this;
                    $points = $(this.$el).find('.progress-points').first();
                    this.activate(this.startPoint);                     
                    $points.on('click', 'li', function (event) {
                        var _index;
                        $point_arr = $(othis.$el).find('.progress-point');
                        _index = $point_arr.index(this);
                     //  _index === 0 ? 1 : _index === othis.currentPoint ? 0 : othis.currentPoint;
                        return othis.activate(_index);
                    });           
                                    

                });
            },
            updated: function () {

            },
            destroyed: function () {
            },
            methods: {
                activate: function (index) {                  
                    var max = this.points.length;
                    $progress = $(this.$el).find('.progressing').first();
                    $point_arr = $(this.$el).find('.progress-point');
                    if (index !== this.currentPoint) {
                        this.currentPoint = index;
                        var $_active = $point_arr.eq(this.currentPoint);
                        $point_arr
                            .removeClass('completed active')
                            .slice(0, this.currentPoint).addClass('completed');
                    $_active.addClass('active');
            return $progress.css('width', (index / (max-1) * 100) + "%");
    }
},
                handleInput: function (e) {

                },
            },
            computed: {
                getPoints: function () {
                  
                    var max = this.dataPoint.length;
                    var space = 100 / (max-1);
                    
                    var numCallbackRuns = 0;
                    this.dataPoint.forEach(function (element) {
                        element.left = space * numCallbackRuns + "%";
                        numCallbackRuns = numCallbackRuns + 1;
                    });
                    return this.dataPoint;
                }
            }
        });
});