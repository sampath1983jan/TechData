require(
	// configuration
	{
		paths: {
			// the Vue lib
            'Vue': 'https://cdn.jsdelivr.net/npm/vue@2.6.0',
			// Vue RequireJS loader
			'vue': 'https://unpkg.com/requirejs-vue@1.1.5/requirejs-vue',
			//'VueRouter':'https://unpkg.com/vue-router/dist/vue-router'
		}
	},
	// load libs right now
	['Vue', 'vue' ],
	function (Vue, vue, VueRouter) {
		var a = httpVueLoader('/vue/login.vue');		
			var cc = new Vue({
				el: '#lgpage',
				components: {
					'aff': a
				}

		//require(['vue!/vue/login'], function (theApp) {			 
		//	theApp.$mount('#lgpage');
		//	//var a = httpVueLoader('/vue/login.vue');			
		//	//var cc = new Vue({
		//	//	el: '#lgpage',
		//	//	components: {
		//	//		'where': a
		//	//	}
		//	//});
		});
	}
);

