$(document).ready(function () {
	(function ($md) {
		$md.ComponentModal = function (options) {
			var defaults = {
				plc: "",
				
			};
			var rsElement = [];
			var _opt = $.extend({}, defaults, options);
			var complist = [];
			var modal = {
				ComponentModalID: "",
				ModalName: "",
				Category: "",
				ParentComponent: "",
				ComponentModalRoot: []
			};
			var node = {
				ComponentModalID: "",
				ComponentModalItemID: "",
				Relations: [],
				ComponentID: "",
				ChildComponentID: "",
			};
			var relation = {
				ParentField: "",
				RelatedField: "",
				Parent: "",
				Child: ""
			};

			var componentmodal = {};
			var selectedModalID = "";
			var getComponentModal = function () {
				componentmodal = $.extend({}, modal);
			};
			var getModalList = function (onComplete) {
				if (window.clientid === "") {
					return;
				}
				$.ajax('/ComponentManager/GetModalList',
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
			};
			var UpdateModal = function (onComplete) {
				if (window.clientid === "") {
					return;
				}
				componentmodal.ModalName = $("#plc_ctrl_name").Input("text");
				componentmodal.Category = $("#plc_ctrl_category").Input("text");
				$.ajax('/ComponentManager/UpdateModal',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({
							clientid: window.clientid,
							modalid: selectedModalID,
							name: componentmodal.ModalName,
							category: componentmodal.Category,
							parentcomponent: "",
							nodes: JSON.stringify(componentmodal.ComponentModalRoot)
						}),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							onComplete(data);
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			};
			var SaveModal = function (onComplete) {
				if (window.clientid === "") {
					return;
				}
				componentmodal.ModalName = $("#plc_ctrl_name").Input("text");
				componentmodal.Category = $("#plc_ctrl_category").Input("text");
				$.ajax('/ComponentManager/SaveModal',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({
							clientid: window.clientid,
							name: componentmodal.ModalName,
							category: componentmodal.Category,
							parentcomponent: "",
							nodes: JSON.stringify(componentmodal.ComponentModalRoot)
						}),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							onComplete(data);
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			};
			var getComponentList = function (onComplete) {
				if (window.clientid === "") {
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
			};
			var renderTableList = function ($plc, data) {
				$plc.html("");
				//list-group
				var sq = "<div class='list'><div class='header'>Component List</div><ul class=''>"
				$.each(data, function (id, d) {
					sq = sq + "<li name='" + d.ComponentID + "' class='ui-cytoscape-nodeadd-icon'><div class='row rowlight'><div class='col-md-10'>" + d.ComponentName + "</div><div class='col-md-2' ><a href='#' style='text-indent:-100%;' name='" + d.ComponentID + "'><span title='Delete' class='remove fa fa-remove'   ></span></a ></div></div></li>"
					//   sq = sq + "<li name='" + d.ComponentID + "'>" + d.ComponentName +"</li>";
				});
				sq = sq + "</ul></div>";
				$plc.append(sq);


				$plc.find("li").click(function () {
					tbID = $(this).attr("name");
					var componentid = tbID;
					$(this).parent().find("li").children().removeClass("liactive");
					$(this).children().removeClass("liactive");
					$(this).children().addClass("liactive");
				});
			};
			var getComponent = function (compid, onComplete) {
				//var getTable = function (onComplete) {
				$.ajax('/ComponentManager/GetComponent',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, componentID: compid }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							data = data.Component;
							var c = {};
							c.Name = data.ComponentName;
							c.Category = data.Category;
							c.title = data.Title;
							c.ComponentType = data.ComponentType;
							c.ComponentID = data.ComponentID;
							c.Fields = [];
							//_opt.Fields = [];
							var fld = [];
							$.each(data.Attributes, function (id, d) {
								var f = {};
								f.FieldID = d.FieldID;
								f.FieldName = d.FieldName;
								f.FieldType = d.FieldType;
								f.Length = d.Length;
								c.Fields.push(f);
							});
							onComplete(c);
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
				//}
			};
			var renderRelation = function (items, c, rc) {
				$("#rsItemplc").html("");
				$.each(items, function (k, v) {
					if (v.IsRemoved == undefined) { v.IsRemoved = false; }
					if (v.IsRemoved == false) {
						getrelationTemplate(k, v, items, c, rc);
					}					
				});
			};
			var getrelationTemplate = function (index, rsItem, rsItems, c, rc) {
				var itm = "";
				itm = itm + "<div class='row' id='rw_" + index + "'>";
				itm = itm + "<div class='col-md-2' id='ttComp" + index + "'>" + rsItem.ParentName + "</div>";
				itm = itm + "<div class='col-md-4' id='cmbCompField" + index + "'></div>";
				itm = itm + "<div class='col-md-2' id='ttreComp" + index + "'>" + rsItem.ChildName + "</div>";
				itm = itm + "<div class='col-md-3' id='cmbCompRelatedField" + index + "'></div>";
				itm = itm + "<div class='col-md-1' id='rmItm_" + index + "'><a><span title='Delete' class='remove fa fa-remove'></span></a></div>";
				itm = itm + "</div>";
				$("#rsItemplc").append(itm);
				renderComponentfields(c.Fields, "cmbCompField" + index, index, rsItem.ParentField);
				renderRelatedComponentfields(rc.Fields, "cmbCompRelatedField" + index, index, rsItem.RelatedField);
				$("#rmItm_" + index).click(function () {
					//rsItems.splice(this.id.split("_")[1], 1);
					rsItems[this.id.split("_")[1]].IsRemoved = true;					
					renderRelation(rsItems, c, rc);
				});
			};
			var changeRelation = function (mitemid, rs) {
				//var getTable = function (onComplete) {
				$.ajax('/ComponentManager/ChangeRelation',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, modalid: selectedModalID, modalitemid: mitemid, relation: rs }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function

						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			}
			var AddNode = function (rs) {
				//var getTable = function (onComplete) {
				$.ajax('/ComponentManager/AddNode',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, modalid: selectedModalID, node:rs }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function

						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			}
			var renderRelationWindow = function (comp, recomp, rsItem,onUpdate) {
				$("#window").modalwindow({
					title: "Add Relationship",
					id: "wd",
					onSave: function () {
						$.each(rsItem, function (k, v) {
							if ($("#cmbCompField" + k).length == 0) {
								return true;
							}
							v.ParentField = $("#cmbCompField" + k).Input("text");
							v.RelatedField = $("#cmbCompRelatedField" + k).Input("text");
							var pfs = c.Fields.filter(function (o) {
								return (o.FieldID == v.ParentField);
							});
							if (pfs.length > 0) {
								v.ParentFieldName = pfs[0].FieldName;
							}
							pfs = [];
							pfs = rc.Fields.filter(function (o) {
								return (o.FieldID == v.RelatedField);
							});
							if (pfs.length > 0) {
								v.RelatedFieldName = pfs[0].FieldName;
							}
						});						 
						var r = [];
						for (i = 0; i < componentmodal.ComponentModalRoot.length; i++) {
							v = componentmodal.ComponentModalRoot[i];
							if ((v.ComponentID === comp && v.ChildComponentID === recomp)) {
								v.Relations = rsItem;								
								//if (v.ComponentModalID != "") {
								//	// update relation
								//	changeRelation(v.ComponentModalID, rsitem);
								//} else if (selectedModalID !=="") {
								//	AddNode(v);
								//}								
								onUpdate(comp, recomp);
								break;
							}
						}
						$("#window").modalwindow("close");
					}
				});

				$("#window").modalwindow("open");
				var itm = "<div><input type='button' class='btn btn-primary' value='Add Relation'></div>";
				itm = itm + "<div id='rsItemplc'></div>"
				$("#window").find(".modal-body").append(itm);
				if (rsItem === undefined) {
					rsItem = [];
				}
				var cindex = rsItem.length;
				var c = {};
				var rc = {};
				getComponent(comp, function (comp) {
					getComponent(recomp, function (relatedcomp) {
						c = comp;
						rc = relatedcomp;
						if (rsItem.length > 0) {
							renderRelation(rsItem, c, rc);
						} else {
							var r = {};
							r.Parent = c.ComponentID;
							r.Child = rc.ComponentID;
							r.ParentName = c.Name;
							r.ChildName = rc.Name;
							r.ParentField = "";
							r.RelatedField = "";
							r.IsRemoved = false;
							//r.Component = c.Name;
							//r.ChildComponent = rc.Name;
							rsItem.push(r);
							
							getrelationTemplate(cindex, r, rsItem, c, rc);

						}

						//$("#ttComp" + cindex).html(comp.Name);
						//$("#ttreComp" + cindex).html(relatedcomp.Name);
					});
				});
				$("#window").find(".modal-body").find("input[type=button]").click(function () {
					cindex = cindex + 1;
					var r = {};
					r.Parent = c.ComponentID;
					r.Child = rc.ComponentID;
					r.ParentName = c.Name;
					r.ChildName = rc.Name;
					r.ParentField = "";
					r.RelatedField = "";
					r.IsRemoved = false;

					//r.Component = c.Name;
					//r.ChildComponent = rc.Name;
					rsItem.push(r);
					getrelationTemplate(cindex, r, rsItem, c, rc);

				});
			};
			var renderRelatedComponentfields = function (flds, plc, cindex, fv) {
				var a = {
					id: "fld_comprelatedfields_" + cindex, 'note': "",
					limit: true, maxlength: 50, inputType: 19, textType: 1, timeformat: 0, 'regexp': "", "text": fv,
					format: "hex", range: false, isRequired: true,
					selectPicker: {
						datasource: flds,
						displayField: "FieldName",
						valueField: "FieldID",
						selection: "single",
						defaultSelection: "Choose Related Field"
					}
				};
				var drpFld = new $("#" + plc).Input(a);
				$("#" + plc).Input("text", fv);
			};
			var renderComponentfields = function (flds, plc, cindex, fv) {
				var a = {
					id: "fld_compfields_" + cindex, 'note': "",
					limit: true, maxlength: 50, inputType: 19, textType: 1, timeformat: 0, 'regexp': "", "text": fv,
					format: "hex", range: false, isRequired: true,
					selectPicker: {
						datasource: flds,
						displayField: "FieldName",
						valueField: "FieldID",
						selection: "single",
						defaultSelection: "Choose Field"
					}
				};
				var drpFld = new $("#" + plc).Input(a);
				$("#" + plc).Input("text", fv);
			};
			var renderComponentList = function () {
				getComponentList(function (data) {
					complist = data;
					renderTableList($("#complist"), data);
					renderGraph(1);
				});
			};
			var renderGraph = function (md) {
				var ele = {};
				ele.nodes = [];				 
				ele.edges = [];
				$.each(componentmodal.ComponentModalRoot, function (k, v) {
					var o = {};
					o.data = {};
					o.data.id = v.ComponentID === "" ? v.ChildComponentID : v.ComponentID;
					o.data.name = v.ParentName === "" ? v.ParentName : v.ParentName;
					ele.nodes.push(o);

					$.each(v.Relations, function (j, val) {
						if (val.ModalItemRelationID == "") {
							return;
						}
						var oo = {};
						oo.data = {};
						oo.data.id = v.ComponentModalItemID; // v.ComponentID + "-" + v.ChildComponentID;// val.ModalItemRelationID;
						oo.data.source = v.ComponentID;
						oo.data.target = v.ChildComponentID;
						oo.data.PName = v.ParentField;
						oo.data.CName = v.Child;
						ele.edges.push(oo);
						return;
					});
				});
				var cy = cytoscape({
					minZoom: 1,
					maxZoom: 2,
					container: document.getElementById("gpcontainer"),
					style: cytoscape.stylesheet()
						.selector('node')
						.style({
							'content': 'data(name)',
							shape: "round-rectangle",
							width: 150,
							height:80,
							'background-opacity': .4,
							'text-halign': "center",
							"text-valign": "center",
							"font-size": 12,
							"source-label": "sdfsd",
							"text-wrap": "wrap",
							"text-max-width": 6
						})

						.selector('edge')
						.style({
							'curve-style': 'taxi',
							'target-arrow-shape': 'triangle',
							'width': 2,
							'line-color': '#ddd',
							'target-arrow-color': ''
						})
						.selector('.highlighted')
						.style({
							'background-color': '#61bffc',
							'line-color': '#61bffc',
							'target-arrow-color': '#ff006e',
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
						}).selector(".multiline-auto").style({
							"text-wrap": "wrap",
							"text-max-width": 20
						})
					, elements: ele,
					//elements: {
					//	nodes: [
					//		{ data: { id: 'j', name: 'Jerry sfds fjsdlkfjdskfj dskf dkfdkf sklf skl fdsklf', classes: 'multiline-auto' } },
					//		{ data: { id: 'e', name: 'Elaine', classes: 'multiline-auto' } },
					//		{ data: { id: 'k', name: 'Kramer', classes: 'multiline-auto' } },
					//		{ data: { id: 'g', name: 'George', classes: 'multiline-auto' } },
					//		{ data: { id: 'y', name: 'sdsdf', classes: 'multiline-auto' } }
					//	],
					//	edges: [
					//		{ data: { id: 'je', source: 'j', target: 'e' } },
					//		{ data: { source: 'j', target: 'k' } },
					//		{ data: { source: 'j', target: 'g' } },
					//		{ data: { source: 'e', target: 'j' } },
					//		{ data: { source: 'e', target: 'k' } },
					//		{ data: { source: 'k', target: 'j' } },
					//		{ data: { source: 'k', target: 'e' } },
					//		{ data: { source: 'k', target: 'g' } },
					//		{ data: { source: 'g', target: 'j' } },
					//		{ data: { source: 'g', target: 'y' } }
					//	],
					//	classes: ['multiline-auto', 'bar']
					//}
					//,
					layout: {
						name: 'breadthfirst',

						fit: true, // whether to fit the viewport to the graph
						padding: 30, // padding used on fit
						boundingBox: undefined, // constrain layout bounds; { x1, y1, x2, y2 } or { x1, y1, w, h }
						avoidOverlap: true, // prevents node overlap, may overflow boundingBox if not enough space
						directed:true,
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
				 
				if (md === 1) {
					cy.contextMenus({
						menuItems: [
							{
								id: 'remove',
								content: 'remove',
								tooltipText: 'remove',
								image: { src: "remove.svg", width: 12, height: 12, x: 6, y: 4 },
								selector: 'node, edge',
								onClickFunction: function (event) {
									var target = event.target || event.cyTarget;									
									var keys = target._private.data.id;
									if (keys.indexOf("-") > 0) {
										var ks = keys.split("-");
										for (i = 0; i < componentmodal.ComponentModalRoot.length; i++) {
											v = componentmodal.ComponentModalRoot[i];
											if (v.ComponentID == ks[0] && v.ChildComponentID == ks[1]) {
												v.Relations = [];
												var a = v.ComponentID;
												var b = v.ChildComponentID;
												$("#" + a +b).remove();												 
											}
										}
									} else {
										for (i = 0; i < componentmodal.ComponentModalRoot.length; i++) {
											v = componentmodal.ComponentModalRoot[i];
											if (v.ComponentModalItemID == keys) {
												var a = v.ComponentID;
												var b = v.ChildComponentID;
												$("#" + a + b).remove();
												removeAllRelation(v.ComponentModalItemID);
											}
											//$.each(v.Relations, function (ir, r) {
											//	if (r.ModalItemRelationID == keys) {
											//		removeAllRelation(v.ComponentModalItemID);
											//		var a =v.ComponentID;
											//		var b = v.ChildComponentID;
											//		$("#" + a + b).remove();
													 
											//	}
											//})
										}
										
									}
									target.remove();
								},
								hasTrailingDivider: true
							},
							{
								id: 'hide',
								content: 'hide',
								tooltipText: 'hide',
								selector: '*',
								onClickFunction: function (event) {
									var target = event.target || event.cyTarget;
									target.hide();
								},
								disabled: false
							},
							{
								id: 'add-node',
								content: 'add node',
								tooltipText: 'add node',
								image: { src: "add.svg", width: 12, height: 12, x: 6, y: 4 },
								coreAsWell: true,
								onClickFunction: function (event) {
									var data = {
										group: 'nodes'
									};

									var pos = event.position || event.cyPosition;

									cy.add({
										data: data,
										position: {
											x: pos.x,
											y: pos.y
										}
									});
								}
							},
							{
								id: 'remove-selected',
								content: 'remove selected',
								tooltipText: 'remove selected',
								image: { src: "remove.svg", width: 12, height: 12, x: 6, y: 6 },
								coreAsWell: true,
								onClickFunction: function (event) {
									cy.$(':selected').remove();
								}
							},
							{
								id: 'select-all-nodes',
								content: 'select all nodes',
								tooltipText: 'select all nodes',
								selector: 'node',
								onClickFunction: function (event) {
									selectAllOfTheSameType(event.target || event.cyTarget);
								}
							},
							{
								id: 'change-relation',
								content: 'Change Relation',
								tooltipText: 'Change Relation',
								selector: 'edge',
								onClickFunction: function (event) {
									//event.target._private.data.id
									var r = [];
									for (i = 0; i < componentmodal.ComponentModalRoot.length; i++) {
										v = componentmodal.ComponentModalRoot[i];
										if ((v.ComponentID == event.target._private.data.source && v.ChildComponentID == event.target._private.data.target)
											|| (v.ComponentID == event.target._private.data.target || v.ChildComponentID == event.target._private.data.source)) {
											r = v.Relations;
											break;
										}
									}
									//$.each(componentmodal.ComponentModalRoot, function (k, v) {
									//	if ((v.ComponentID == event.target._private.data.source && v.ChildComponentID == event.target._private.data.target)
									//		|| (v.ComponentID == event.target._private.data.target || v.ChildComponentID == event.target._private.data.source)) {
									//		r = v.Relations;

									//	}
									//});
									renderRelationWindow(event.target._private.data.source,
										event.target._private.data.target,
										r, function (s,t) {
											updateRelation(s, t);
										});
									//selectAllOfTheSameType(event.target || event.cyTarget);

								}
							},
							{
								id: 'select-all-edges',
								content: 'select all edges',
								tooltipText: 'select all edges',
								selector: 'edge',
								onClickFunction: function (event) {
									//	selectAllOfTheSameType(event.target || event.cyTarget);
								}
							}
						]
					});
					var edgedefaults = {
						preview: true, // whether to show added edges preview before releasing selection
						hoverDelay: 150, // time spent hovering over a target node before it is considered selected
						handleNodes: 'node', // selector/filter function for whether edges can be made from a given node
						snap: false, // when enabled, the edge can be drawn by just moving close to a target node (can be confusing on compound graphs)
						snapThreshold: 50, // the target node must be less than or equal to this many pixels away from the cursor/finger
						snapFrequency: 15, // the number of times per second (Hz) that snap checks done (lower is less expensive)
						noEdgeEventsInDraw: false, // set events:no to edges during draws, prevents mouseouts on compounds
						disableBrowserGestures: true, // during an edge drawing gesture, disable browser gestures such as two-finger trackpad swipe and pinch-to-zoom
						handlePosition: function (node) {
							return 'middle top'; // sets the position of the handle in the format of "X-AXIS Y-AXIS" such as "left top", "middle top"
						},
						handleInDrawMode: false, // whether to show the handle in draw mode
						edgeType: function (sourceNode, targetNode) {
							// can return 'flat' for flat edges between nodes or 'node' for intermediate node between them
							// returning null/undefined means an edge can't be added between the two nodes
							return 'flat';
						},
						loopAllowed: function (node) {
							// for the specified node, return whether edges from itself to itself are allowed
							return false;
						},
						nodeLoopOffset: -50, // offset for edgeType: 'node' loops
						nodeParams: function (sourceNode, targetNode) {
							// for edges between the specified source and target
							// return element object to be passed to cy.add() for intermediary node
							return {};
						},
						edgeParams: function (sourceNode, targetNode, i) {
							// for edges between the specified source and target
							// return element object to be passed to cy.add() for edge
							// NB: i indicates edge index in case of edgeType: 'node'
							return {};
						},
						ghostEdgeParams: function () {
							// return element object to be passed to cy.add() for the ghost edge
							// (default classes are always added for you)
							return {};
						},
						show: function (sourceNode) {
							// fired when handle is shown
						},
						hide: function (sourceNode) {
							// fired when the handle is hidden
						},
						start: function (sourceNode) {
							// fired when edgehandles interaction starts (drag on handle)
						},
						complete: function (sourceNode, targetNode, addedEles) {
							// fired when edgehandles is done and elements are added						 
							for (i = 0; i < componentmodal.ComponentModalRoot.length; i++) {
								var a = componentmodal.ComponentModalRoot[i];
								if (a.ComponentID == sourceNode._private.data.id) {
									a.ChildComponentID = targetNode._private.data.id;
								};
							}
							renderRelationWindow(sourceNode._private.data.id, targetNode._private.data.id,
								undefined, function (s,t) {
									updateRelation(s,t);
								});
							updateRelation(sourceNode._private.data.id, targetNode._private.data.id);
						},
						stop: function (sourceNode) {
							// fired when edgehandles interaction is stopped (either complete with added edges or incomplete)

						},
						cancel: function (sourceNode, cancelledTargets) {
							// fired when edgehandles are cancelled (incomplete gesture)
						},
						hoverover: function (sourceNode, targetNode) {
							// fired when a target is hovered
						},
						hoverout: function (sourceNode, targetNode) {
							// fired when a target isn't hovered anymore
						},
						previewon: function (sourceNode, targetNode, previewEles) {
							// fired when preview is shown
						},
						previewoff: function (sourceNode, targetNode, previewEles) {
							// fired when preview is hidden
						},
						drawon: function () {
							// fired when draw mode enabled
						},
						drawoff: function () {
							// fired when draw mode disabled
						}
					};
					var eh = cy.edgehandles(edgedefaults);				 
					var defaults = {
						height: 120,   //height of the icon container
						width: 100,    //width of the icon container
						padding: 3,  //padding of the icon container(from right & top)
						backgroundColorDiv: 'yellow',   //background color of the icon container
						borderColorDiv: 'red',    //border color of the icon container
						borderWidthDiv: '1px',    //border width of the icon container
						borderRadiusDiv: '5px',    //border radius of the icon container
						icon: 'fa fa-circle fa-2x',   //icon class name
						Components: complist,
						onAdded: function (a) {
							var n = $.extend({}, node);
							n.ComponentID = a.id;
							n.ComponentName = a.name;
							n.ChildComponentID = "";
							componentmodal.ComponentModalRoot.push(n);
						}
					};
					cy.$('node').on('grab', function (e) {
						var ele = e.target;
						//  ele.connectedEdges().style({ 'line-color': 'red' });
					});
					cy.nodeadd(defaults);
				}
				var updateRelation = function (s, t) {
				var acc=	componentmodal.ComponentModalRoot.filter(function (o) {
						return ((o.ComponentID == s && o.ChildComponentID == t));
				});
					var abc = "";
					if (acc.length > 0) {
						if (acc[0].Relations.length == 0) {
							return;
						}
						if (acc[0].ComponentModalItemID == "") {
							abc = cy.getElementById(s + "-" + t);
						} else {
							abc = cy.getElementById(acc[0].ComponentModalItemID);
							if (abc.length == 0) {
								abc = cy.getElementById(s + "-" + t);
							}
						}						
					} else {
						abc = cy.getElementById(s + "-" + t);
						return "";
					}				

					var k = componentmodal.ComponentModalRoot.filter(function (obj) {
						return (obj.ComponentID === s);
					});
					if (k.length > 0) {
						k[0].ChildComponentID = t;
					};

					
						var popperABc = abc.popper({
							content: function (a) { return makeDiv(a, 'Sticky position div'); }
						});
					//if (abc.pop == undefined) {
						abc.pop = popperABc;
						rsElement.push(abc);
					///} 			
					
					abc.updateAB = function () {
						this.pop.scheduleUpdate();
					};
					abc.connectedNodes().on('position', function () {
						//	this.popperAB.scheduleUpdate();
						$.each(rsElement, function (k, v) {
							v.updateAB();
						});
					});
					
					cy.on('pan zoom resize', function () {
						$.each(rsElement, function (k, v) {
							v.updateAB();
						});
					});
					//cy.on('pan zoom resize', updateAB);
				};
				var makeDiv = function (t, text) {
					var s = t._private.data.source;
					var t = t._private.data.target;
					var a = s;
					var b = t;

					var acc = componentmodal.ComponentModalRoot.filter(function (o) {
						return ((o.ComponentID == a && o.ChildComponentID == b));
					});
					var fs = "";
					if (acc.length > 0) {
						var sr = acc[0].Relations;
						if (sr.length > 0) {
							$.each(acc[0].Relations, function (k, v) {
								fs = fs + "<div class='row first-child'><div>" + v.ParentFieldName + "</div> <i class='fa fa-arrows-h'></i> <div>" + v.RelatedFieldName + "</div></div>";
							});
							
						} else {
							return "";
						}
					} else {
						return "";
						//var aa = complist.filter(function (obj) {
						//	return (obj.ComponentID == a);
						//});
						//if (aa.length > 0) {
						//	a = aa[0].ComponentName;
						//}

						//aa = complist.filter(function (obj) {
						//	return (obj.ComponentID == b);
						//});
						//if (aa.length > 0) {
						//	b = aa[0].ComponentName;
						//}
					};


					$("#" + s + t).remove();

					var div = document.createElement('div');					
					div.classList.add('popper-div');
					div.innerHTML = fs;
					document.body.appendChild(div);
					$(div).attr("id", s + t);
					return div;
				};
				$.each(componentmodal.ComponentModalRoot, function (k, v) {
					if (v.ChildComponentID != "") {
						updateRelation(v.ComponentID, v.ChildComponentID);
					}
				});				
			};
			var clr = function () {
				$(".popper-div").remove();
			};
			var removeAllRelation = function (ModalItemID) {
				if (window.clientid === "") {
					return;
				}
				$.ajax('/ComponentManager/RemoveItemAllRelation',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, modalid: selectedModalID, modalitemid: ModalItemID }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							 
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			}
			var removeRelation = function (ModalItemID, rsid, onComplete) {
				if (window.clientid === "") {
					return;
				}
				$.ajax('/ComponentManager/RemoveItemRelation',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, modalid: selectedModalID, modalitemid: ModalItemID, relation: rsid }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							onComplete();
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			};
			var removeModal = function (slmdid,onComplete) {
				if (window.clientid === "") {
					return;
				}
				$.ajax('/ComponentManager/RemoveModal',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, modalid: slmdid }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							onComplete();
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			};
			var renderNewModal = function (mode) {
				$("#" + _opt.plc).html("");
				$("#" + _opt.plc).append("<div class='row'><div class='col-md-3' id='complist'></div><div   class='col-md-9' style='border-left:4px solid #eee;' id='mdcontainer'></div></div>");
				$("#mdcontainer").append("<div id='mdcontaineratt' ></div><div style='width:850px; height:510px' id='gpcontainer'></div>");
				$("#mdcontainer").append('<div class="col-md-12"><button type="button" class="btn btn-primary" action="mdSave">Save</button> <button type="button" class="btn btn-default" action="can">Cancel</button></div>');
				var itm = "<div id='plc_ctrl_name' class='col-md-6'></div>";
				itm = itm + "<div id='plc_ctrl_category' class='col-md-5'></div>";
				$("#mdcontaineratt").append("<div class='row'>" + itm + "</div>");
				if (mode == 1) {
					getComponentModal();
				}				
				renderComponentList();
			 
				$("#mdcontainer").find("[action=can]").click(function () {
					clr();
					render();
				});
				$("#mdcontainer").find("[action=mdSave]").click(function () {
					if (componentmodal.ComponentModalID != "") {
						UpdateModal(function (cmodal) {							 
							render();
						});
					} else {
						SaveModal(function (cmodal) {
							selectedModalID = cmodal;
							render();
						});
					}
					
				});
				$("#mdcontaineratt").labelfields({
					items: [{
						label: "Modal Name",
						field: {
							id: "fldmdName", 'note': "Enter Modal Name",
							limit: true, maxlength: 50, inputType: 1, textType: 1, timeformat: 0, 'regexp': "", "text": componentmodal.ModalName,
							format: "hex", range: false, isRequired: true,
						},
						id: 'plc_ctrl_name'
					}, {
						label: "Modal Category",
						field: {
							id: "fldmdcategory", 'note': "Enter Category Name",
							limit: true, maxlength: 50, inputType: 1, textType: 1, timeformat: 0, 'regexp': "", "text": componentmodal.Category,
							format: "hex", range: false, isRequired: true,
						},
						id: 'plc_ctrl_category'
					}
					]
				});
				renderGraph(1); // edit, new
			};
			var getModalDetail = function (onComplete) {
				if (window.clientid === "") {
					return;
				}
				$.ajax('/ComponentManager/GetModal',
					{
						type: "GET",
						dataType: 'jsonp', // type of response data
						data: ({ clientid: window.clientid, modalid: selectedModalID }),
						// timeout milliseconds
						success: function (data, status, xhr) {   // success callback function
							getComponentModal();
							componentmodal.ModalName = data.ModalName;
							componentmodal.Category = data.Category; 
							componentmodal.ComponentModalRoot = data.ComponentModalRoot;
							componentmodal.ComponentModalID = data.ComponentModalID;
							onComplete();
						},
						error: function (jqXhr, textStatus, errorMessage) { // error callback
							alert(errorMessage);
							//$('p').append('Error: ' + errorMessage);
						}
					});
			};		
			var loadModalDetail = function (mode) {
				getModalDetail(function () {
					renderModalDetail();
					$("#gpcontainer").html("");
					renderGraph(mode);
				});

			}
			var renderModalDetail = function () {

				$("#mdcontaineratt").html("");
				if (componentmodal.ModalName == undefined) {
					componentmodal.ModalName = "";
					componentmodal.Category = "";
				}
				var s = "<div class='col-md-8'> <div><h5>" + componentmodal.ModalName + "<span  class='badge badge-secondary'>" + componentmodal.Category + "</span> </h5></div></div>";
				s = s + '<div class="col-md-4 btn-group" > <button type="button" class="btn btn-primary" action="mdEdit"><i class="fa fa-edit" style="margin-right:5px"></i>Edit</button><button type="button" class="btn btn-secondary" action="mdNew"><i class="fa fa-plus" style="margin-right:5px"></i>New</button> <button type="button" class="btn btn-secondary " action="rmmodal"><i class="fa fa-times" style="margin-right:5px"></i>Remove</button></div>';
				$("#mdcontaineratt").append(s);
				$("#mdcontaineratt").find("[action=mdNew]").click(function () {
					clr();
					renderNewModal(1);//new
				});
				$("#mdcontaineratt").find("[action=mdEdit]").click(function () {
					clr();
					 renderNewModal(2);//edit
					//render();
				});
				$("#mdcontaineratt").find("[action=rmmodal]").click(function () {					
					removeModal(selectedModalID,function () {
						clr();
						selectedModalID = "";
						render();
					});
					//render();
				});
				
			};
			var renderModalList = function (data) {
				$("#" + _opt.plc).append("<div class='row'><div class='col-md-3' id='cmdlist'></div><div   class='col-md-9' style='border-left:4px solid #eee;' id='mdcontainer'></div></div>");
				$("#mdcontainer").append("<div id='mdcontaineratt' class='row' ></div><div style='width:850px; height:510px' id='gpcontainer'></div>");
				//$.each(data, function (k, v) {
				var sq = "<div class='list'><div class='header'>Modal List</div><ul class=''>";
					$.each(data, function (id, d) {
						sq = sq + "<li name='" + d.ComponentModalID + "' class='ui-cytoscape-nodeadd-icon'><div class='row rowlight'><div class='col-md-10'>" + d.ModalName + "</div><div class='col-md-2' ><a href='#' style='text-indent:-100%;' name='" + d.ComponentModalID + "'><span title='Delete' act='del' class='remove fa fa-remove'   ></span></a ></div></div></li>";						
					});
					sq = sq + "</ul></div>";
				$("#cmdlist").append(sq);
				renderModalDetail();
				$("#cmdlist").find("[act=del]").click(function (e) {
					e.stopPropagation();
					var mdid = $(this).parent().attr("name");
					clr();
					removeModal(mdid, function () {						
						$("#cmdlist").find("li[name=" + mdid + "]").remove();						 
					});
				})
				$("#cmdlist").find("li").click(function () {
					selectedModalID = $(this).attr("name");
					clr();
					//componentmodal.ComponentModalID = selectedModalID;
					loadModalDetail(2);
				});
			
				//});
				//renderGraph(2);
			};			
			var render = function () {
				$("#" + _opt.plc).css("margin-top","10px");
				$("#" + _opt.plc).html("");
				//$("#" + _opt.plc).append("<div></div>");
				getModalList(function (data) {
					renderModalList(data);
				});	
			};
			render();

		};
	})($);
});

 