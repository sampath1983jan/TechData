$(document).ready(function () {
(function ($md) {
	$md.ComponentModal = function (options) {
		var defaults = {
			plc: ""
		};
		var _opt = $.extend({}, defaults, options);
		var getComponentList = function (onComplete) {
			if (window.clientid == "") {
				return;
			}
			$.ajax('/ComponentManager/GetComponents',
				{
					type: "GET",
					dataType: 'jsonp', // type of response data
					data: ({ clientid: window.clientid }),
					// timeout milliseconds
					success: function (data, status, xhr) {   // success callback function
						onComplete(data);
					},
					error: function (jqXhr, textStatus, errorMessage) { // error callback
						alert(errorMessage);
						//$('p').append('Error: ' + errorMessage);
					}
				});
		}
		var renderTableList = function ($plc, data) {
			$plc.html("");
			//list-group
			var sq = "<div class='list'><div class='header'>Component List</div><ul class=''>"
			$.each(data, function (id, d) {
				sq = sq + "<li name='" + d.ComponentID + "'><div class='row rowlight'><div class='col-md-10'>" + d.ComponentName + "</div><div class='col-md-2' ><a href='#' style='text-indent:-100%;' name='" + d.ComponentID + "'><span title='Delete' class='remove fa fa-remove'   ></span></a ></div></div></li>"
				//   sq = sq + "<li name='" + d.ComponentID + "'>" + d.ComponentName +"</li>";
			});
			sq = sq + "</ul></div>"
			$plc.append(sq);


			$plc.find("li").click(function () {
				tbID = $(this).attr("name");
				var componentid = tbID;
				$(this).parent().find("li").children().removeClass("liactive");
				$(this).children().removeClass("liactive");
				$(this).children().addClass("liactive");
			});
		}
		var renderComponentList = function () {
			getComponentList(function (data) {
				renderTableList($("#complist"), data);
			});
		}
		var renderGraph = function () {
			var cy = cytoscape({

				container: document.getElementById("gpcontainer"),
				style: cytoscape.stylesheet()
					.selector('node')
					.style({
						'content': 'data(name)',
						shape: "round-rectangle",
						width: 50,
						height: 20,
						'background-opacity': .4,
						'text-halign': "center",
						"text-valign": "center",
						"font-size": 10,
						"source-label": "sdfsd"
					})

					.selector('edge')
					.style({
						'curve-style': 'straight',
						'target-arrow-shape': 'triangle',
						'width': 2,
						'line-color': '#ddd',
						'target-arrow-color': ''
					})
					.selector('.highlighted')
					.style({
						'background-color': '#61bffc',
						'line-color': '#61bffc',
						'target-arrow-color': '#61bffc',
						'transition-property': 'background-color, line-color, target-arrow-color',
						'transition-duration': '0.5s'
					}).selector(".eh-hover")
					.style({
						'background-color': '#ddd'
					}).selector(".eh-handle")
					.style({
						'background-color': 'green',
						'width': 12,
						'height': 12,
						'shape': 'ellipse',
						'overlay-opacity': 0,
						'border-width': 12, // makes the handle easier to hit
						'border-opacity': 0
					}).selector(".eh-source")
					.style({
						'border-width': 1,
						'border-color': 'yellow'
					}).selector(".eh-target")
					.style({
						'border-width': 1,
						'border-color': 'red'
					}).selector(".eh-preview, .eh-ghost-edge")
					.style({
						'background-color': 'red',
						'line-color': '#ff6a00',
						'target-arrow-color': 'red',
						'source-arrow-color': 'red'
					}).selector(".eh-ghost-edge.eh-preview-active")
					.style({
						'opacity': 0
					})
				,

				elements: {
					nodes: [
						{ data: { id: 'j', name: 'Jerry' } },
						{ data: { id: 'e', name: 'Elaine' } },
						{ data: { id: 'k', name: 'Kramer' } },
						{ data: { id: 'g', name: 'George' } },
						{ data: { id: 'y', name: 'sdsdf' } }
					],
					edges: [
						{ data: { source: 'j', target: 'e' } },
						{ data: { source: 'j', target: 'k' } },
						{ data: { source: 'j', target: 'g' } },
						{ data: { source: 'e', target: 'j' } },
						{ data: { source: 'e', target: 'k' } },
						{ data: { source: 'k', target: 'j' } },
						{ data: { source: 'k', target: 'e' } },
						{ data: { source: 'k', target: 'g' } },
						{ data: { source: 'g', target: 'j' } },
						{ data: { source: 'g', target: 'y' } }
					]
				},
				layout: {
					name: 'grid',

					fit: true, // whether to fit the viewport to the graph
					padding: 30, // padding used on fit
					boundingBox: undefined, // constrain layout bounds; { x1, y1, x2, y2 } or { x1, y1, w, h }
					avoidOverlap: true, // prevents node overlap, may overflow boundingBox if not enough space
					avoidOverlapPadding: 10, // extra spacing around nodes when avoidOverlap: true
					nodeDimensionsIncludeLabels: false, // Excludes the label when calculating node bounding boxes for the layout algorithm
					spacingFactor: undefined, // Applies a multiplicative factor (>0) to expand or compress the overall area that the nodes take up
					condense: false, // uses all available space on false, uses minimal space on true
					rows: undefined, // force num of rows in the grid
					cols: undefined, // force num of columns in the grid
					position: function (node) { }, // returns { row, col } for element
					sort: undefined, // a sorting function to order the nodes; e.g. function(a, b){ return a.data('weight') - b.data('weight') }
					animate: true, // whether to transition the node positions
					animationDuration: 500, // duration of animation in ms if enabled
					animationEasing: undefined, // easing of animation if enabled
					animateFilter: function (node, i) { return true; }, // a function that determines whether the node should be animated.  All nodes animated by default on animate enabled.  Non-animated nodes are positioned immediately when the layout starts
					startAngle: 3 / 2 * Math.PI,
					ready: undefined, // callback on layoutready
					stop: undefined, // callback on layoutstop
					transform: function (node, position) { return position; }
				}

			});

			cy.add([
				{ group: "nodes", data: { id: "p", name: "mytesting", weight: 75 }, position: { x: 200, y: 200 } },
				{ group: "nodes", data: { id: "h", name: "node1", weight: 75 }, position: { x: 300, y: 200 } },
				{ group: "edges", data: { id: "e0", source: "p", target: "h", label: "sampathkumar" }, classes: 'autorotate' },
				{ group: "edges", data: { id: "e10", source: "j", target: "p" } },
				{ group: "edges", data: { id: "e110", source: "h", target: "j" } }
			]);

			var eh = cy.edgehandles();
		 

			var defaults = {
				height: 30,   //height of the icon container
				width: 30,    //width of the icon container
				padding: 3,  //padding of the icon container(from right & top)
				backgroundColorDiv: 'yellow',   //background color of the icon container
				borderColorDiv: 'red',    //border color of the icon container
				borderWidthDiv: '1px',    //border width of the icon container
				borderRadiusDiv: '5px',    //border radius of the icon container

				icon: 'fa fa-circle fa-2x',   //icon class name
			};
			cy.$('node').on('grab', function (e) {
				var ele = e.target;
				//  ele.connectedEdges().style({ 'line-color': 'red' });
			});
			cy.nodeadd(defaults);
		}
		var render = function () {
			$("#" + _opt.plc).append("<div class='row'><div class='col-md-3' id='complist'></div><div  style='width:300px; height:600px' class='col-md-9' id='gpcontainer'></div></div>");
			renderComponentList();
			renderGraph();
		}
		render();

	}
})($);
})

 