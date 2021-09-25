
layui.define(['form'], function (exports) { //提示：模块也可以依赖其它模块，如：layui.define('layer', callback);
    // var jQuery = layui.jquery,
    //     $ = jQuery,
    var form = layui.form,
        _MOD = 'treeSelect',
        trss = {},
        TreeSelect = function () {
            this.v = '1.0.5';
        };


    /*
 * JQuery zTree core v3.5.40
 * http://treejs.cn/
 *
 * Copyright (c) 2010 Hunter.z
 *
 * Licensed same as jquery - MIT License
 * http://www.opensource.org/licenses/mit-license.php
 *
 * email: hunter.z@263.net
 * Date: 2019-01-18
 */
    (function ($) {
        var settings = {}, roots = {}, caches = {},
            //default consts of core
            _consts = {
                className: {
                    BUTTON: "button",
                    LEVEL: "level",
                    ICO_LOADING: "ico_loading",
                    SWITCH: "switch",
                    NAME: 'node_name'
                },
                event: {
                    NODECREATED: "ztree_nodeCreated",
                    CLICK: "ztree_click",
                    EXPAND: "ztree_expand",
                    COLLAPSE: "ztree_collapse",
                    ASYNC_SUCCESS: "ztree_async_success",
                    ASYNC_ERROR: "ztree_async_error",
                    REMOVE: "ztree_remove",
                    SELECTED: "ztree_selected",
                    UNSELECTED: "ztree_unselected"
                },
                id: {
                    A: "_a",
                    ICON: "_ico",
                    SPAN: "_span",
                    SWITCH: "_switch",
                    UL: "_ul"
                },
                line: {
                    ROOT: "root",
                    ROOTS: "roots",
                    CENTER: "center",
                    BOTTOM: "bottom",
                    NOLINE: "noline",
                    LINE: "line"
                },
                folder: {
                    OPEN: "open",
                    CLOSE: "close",
                    DOCU: "docu"
                },
                node: {
                    CURSELECTED: "curSelectedNode"
                }
            },
            //default setting of core
            _setting = {
                treeId: "",
                treeObj: null,
                view: {
                    addDiyDom: null,
                    autoCancelSelected: true,
                    dblClickExpand: true,
                    expandSpeed: "fast",
                    fontCss: {},
                    nameIsHTML: false,
                    selectedMulti: true,
                    showIcon: true,
                    showLine: true,
                    showTitle: true,
                    txtSelectedEnable: false
                },
                data: {
                    key: {
                        isParent: "isParent",
                        children: "children",
                        name: "name",
                        title: "",
                        url: "url",
                        icon: "icon"
                    },
                    simpleData: {
                        enable: false,
                        idKey: "id",
                        pIdKey: "pId",
                        rootPId: null
                    },
                    keep: {
                        parent: false,
                        leaf: false
                    }
                },
                async: {
                    enable: false,
                    contentType: "application/x-www-form-urlencoded",
                    type: "post",
                    dataType: "text",
                    headers: {},
                    xhrFields: {},
                    url: "",
                    autoParam: [],
                    otherParam: [],
                    dataFilter: null
                },
                callback: {
                    beforeAsync: null,
                    beforeClick: null,
                    beforeDblClick: null,
                    beforeRightClick: null,
                    beforeMouseDown: null,
                    beforeMouseUp: null,
                    beforeExpand: null,
                    beforeCollapse: null,
                    beforeRemove: null,

                    onAsyncError: null,
                    onAsyncSuccess: null,
                    onNodeCreated: null,
                    onClick: null,
                    onDblClick: null,
                    onRightClick: null,
                    onMouseDown: null,
                    onMouseUp: null,
                    onExpand: null,
                    onCollapse: null,
                    onRemove: null
                }
            },
            //default root of core
            //zTree use root to save full data
            _initRoot = function (setting) {
                var r = data.getRoot(setting);
                if (!r) {
                    r = {};
                    data.setRoot(setting, r);
                }
                data.nodeChildren(setting, r, []);
                r.expandTriggerFlag = false;
                r.curSelectedList = [];
                r.noSelection = true;
                r.createdNodes = [];
                r.zId = 0;
                r._ver = (new Date()).getTime();
            },
            //default cache of core
            _initCache = function (setting) {
                var c = data.getCache(setting);
                if (!c) {
                    c = {};
                    data.setCache(setting, c);
                }
                c.nodes = [];
                c.doms = [];
            },
            //default bindEvent of core
            _bindEvent = function (setting) {
                var o = setting.treeObj,
                    c = consts.event;
                o.bind(c.NODECREATED, function (event, treeId, node) {
                    tools.apply(setting.callback.onNodeCreated, [event, treeId, node]);
                });

                o.bind(c.CLICK, function (event, srcEvent, treeId, node, clickFlag) {
                    tools.apply(setting.callback.onClick, [srcEvent, treeId, node, clickFlag]);
                });

                o.bind(c.EXPAND, function (event, treeId, node) {
                    tools.apply(setting.callback.onExpand, [event, treeId, node]);
                });

                o.bind(c.COLLAPSE, function (event, treeId, node) {
                    tools.apply(setting.callback.onCollapse, [event, treeId, node]);
                });

                o.bind(c.ASYNC_SUCCESS, function (event, treeId, node, msg) {
                    tools.apply(setting.callback.onAsyncSuccess, [event, treeId, node, msg]);
                });

                o.bind(c.ASYNC_ERROR, function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                    tools.apply(setting.callback.onAsyncError, [event, treeId, node, XMLHttpRequest, textStatus, errorThrown]);
                });

                o.bind(c.REMOVE, function (event, treeId, treeNode) {
                    tools.apply(setting.callback.onRemove, [event, treeId, treeNode]);
                });

                o.bind(c.SELECTED, function (event, treeId, node) {
                    tools.apply(setting.callback.onSelected, [treeId, node]);
                });
                o.bind(c.UNSELECTED, function (event, treeId, node) {
                    tools.apply(setting.callback.onUnSelected, [treeId, node]);
                });
            },
            _unbindEvent = function (setting) {
                var o = setting.treeObj,
                    c = consts.event;
                o.unbind(c.NODECREATED)
                    .unbind(c.CLICK)
                    .unbind(c.EXPAND)
                    .unbind(c.COLLAPSE)
                    .unbind(c.ASYNC_SUCCESS)
                    .unbind(c.ASYNC_ERROR)
                    .unbind(c.REMOVE)
                    .unbind(c.SELECTED)
                    .unbind(c.UNSELECTED);
            },
            //default event proxy of core
            _eventProxy = function (event) {
                var target = event.target,
                    setting = data.getSetting(event.data.treeId),
                    tId = "", node = null,
                    nodeEventType = "", treeEventType = "",
                    nodeEventCallback = null, treeEventCallback = null,
                    tmp = null;

                if (tools.eqs(event.type, "mousedown")) {
                    treeEventType = "mousedown";
                } else if (tools.eqs(event.type, "mouseup")) {
                    treeEventType = "mouseup";
                } else if (tools.eqs(event.type, "contextmenu")) {
                    treeEventType = "contextmenu";
                } else if (tools.eqs(event.type, "click")) {
                    if (tools.eqs(target.tagName, "span") && target.getAttribute("treeNode" + consts.id.SWITCH) !== null) {
                        tId = tools.getNodeMainDom(target).id;
                        nodeEventType = "switchNode";
                    } else {
                        tmp = tools.getMDom(setting, target, [{ tagName: "a", attrName: "treeNode" + consts.id.A }]);
                        if (tmp) {
                            tId = tools.getNodeMainDom(tmp).id;
                            nodeEventType = "clickNode";
                        }
                    }
                } else if (tools.eqs(event.type, "dblclick")) {
                    treeEventType = "dblclick";
                    tmp = tools.getMDom(setting, target, [{ tagName: "a", attrName: "treeNode" + consts.id.A }]);
                    if (tmp) {
                        tId = tools.getNodeMainDom(tmp).id;
                        nodeEventType = "switchNode";
                    }
                }
                if (treeEventType.length > 0 && tId.length == 0) {
                    tmp = tools.getMDom(setting, target, [{ tagName: "a", attrName: "treeNode" + consts.id.A }]);
                    if (tmp) {
                        tId = tools.getNodeMainDom(tmp).id;
                    }
                }
                // event to node
                if (tId.length > 0) {
                    node = data.getNodeCache(setting, tId);
                    switch (nodeEventType) {
                        case "switchNode":
                            var isParent = data.nodeIsParent(setting, node);
                            if (!isParent) {
                                nodeEventType = "";
                            } else if (tools.eqs(event.type, "click")
                                || (tools.eqs(event.type, "dblclick") && tools.apply(setting.view.dblClickExpand, [setting.treeId, node], setting.view.dblClickExpand))) {
                                nodeEventCallback = handler.onSwitchNode;
                            } else {
                                nodeEventType = "";
                            }
                            break;
                        case "clickNode":
                            nodeEventCallback = handler.onClickNode;
                            break;
                    }
                }
                // event to zTree
                switch (treeEventType) {
                    case "mousedown":
                        treeEventCallback = handler.onZTreeMousedown;
                        break;
                    case "mouseup":
                        treeEventCallback = handler.onZTreeMouseup;
                        break;
                    case "dblclick":
                        treeEventCallback = handler.onZTreeDblclick;
                        break;
                    case "contextmenu":
                        treeEventCallback = handler.onZTreeContextmenu;
                        break;
                }
                var proxyResult = {
                    stop: false,
                    node: node,
                    nodeEventType: nodeEventType,
                    nodeEventCallback: nodeEventCallback,
                    treeEventType: treeEventType,
                    treeEventCallback: treeEventCallback
                };
                return proxyResult
            },
            //default init node of core
            _initNode = function (setting, level, n, parentNode, isFirstNode, isLastNode, openFlag) {
                if (!n) return;
                var r = data.getRoot(setting),
                    children = data.nodeChildren(setting, n);
                n.level = level;
                n.tId = setting.treeId + "_" + (++r.zId);
                n.parentTId = parentNode ? parentNode.tId : null;
                n.open = (typeof n.open == "string") ? tools.eqs(n.open, "true") : !!n.open;
                var isParent = data.nodeIsParent(setting, n);
                if (tools.isArray(children)) {
                    data.nodeIsParent(setting, n, true);
                    n.zAsync = true;
                } else {
                    isParent = data.nodeIsParent(setting, n, isParent);
                    n.open = (isParent && !setting.async.enable) ? n.open : false;
                    n.zAsync = !isParent;
                }
                n.isFirstNode = isFirstNode;
                n.isLastNode = isLastNode;
                n.getParentNode = function () {
                    return data.getNodeCache(setting, n.parentTId);
                };
                n.getPreNode = function () {
                    return data.getPreNode(setting, n);
                };
                n.getNextNode = function () {
                    return data.getNextNode(setting, n);
                };
                n.getIndex = function () {
                    return data.getNodeIndex(setting, n);
                };
                n.getPath = function () {
                    return data.getNodePath(setting, n);
                };
                n.isAjaxing = false;
                data.fixPIdKeyValue(setting, n);
            },
            _init = {
                bind: [_bindEvent],
                unbind: [_unbindEvent],
                caches: [_initCache],
                nodes: [_initNode],
                proxys: [_eventProxy],
                roots: [_initRoot],
                beforeA: [],
                afterA: [],
                innerBeforeA: [],
                innerAfterA: [],
                zTreeTools: []
            },
            //method of operate data
            data = {
                addNodeCache: function (setting, node) {
                    data.getCache(setting).nodes[data.getNodeCacheId(node.tId)] = node;
                },
                getNodeCacheId: function (tId) {
                    return tId.substring(tId.lastIndexOf("_") + 1);
                },
                addAfterA: function (afterA) {
                    _init.afterA.push(afterA);
                },
                addBeforeA: function (beforeA) {
                    _init.beforeA.push(beforeA);
                },
                addInnerAfterA: function (innerAfterA) {
                    _init.innerAfterA.push(innerAfterA);
                },
                addInnerBeforeA: function (innerBeforeA) {
                    _init.innerBeforeA.push(innerBeforeA);
                },
                addInitBind: function (bindEvent) {
                    _init.bind.push(bindEvent);
                },
                addInitUnBind: function (unbindEvent) {
                    _init.unbind.push(unbindEvent);
                },
                addInitCache: function (initCache) {
                    _init.caches.push(initCache);
                },
                addInitNode: function (initNode) {
                    _init.nodes.push(initNode);
                },
                addInitProxy: function (initProxy, isFirst) {
                    if (!!isFirst) {
                        _init.proxys.splice(0, 0, initProxy);
                    } else {
                        _init.proxys.push(initProxy);
                    }
                },
                addInitRoot: function (initRoot) {
                    _init.roots.push(initRoot);
                },
                addNodesData: function (setting, parentNode, index, nodes) {
                    var children = data.nodeChildren(setting, parentNode), params;
                    if (!children) {
                        children = data.nodeChildren(setting, parentNode, []);
                        index = -1;
                    } else if (index >= children.length) {
                        index = -1;
                    }

                    if (children.length > 0 && index === 0) {
                        children[0].isFirstNode = false;
                        view.setNodeLineIcos(setting, children[0]);
                    } else if (children.length > 0 && index < 0) {
                        children[children.length - 1].isLastNode = false;
                        view.setNodeLineIcos(setting, children[children.length - 1]);
                    }
                    data.nodeIsParent(setting, parentNode, true);

                    if (index < 0) {
                        data.nodeChildren(setting, parentNode, children.concat(nodes));
                    } else {
                        params = [index, 0].concat(nodes);
                        children.splice.apply(children, params);
                    }
                },
                addSelectedNode: function (setting, node) {
                    var root = data.getRoot(setting);
                    if (!data.isSelectedNode(setting, node)) {
                        root.curSelectedList.push(node);
                    }
                },
                addCreatedNode: function (setting, node) {
                    if (!!setting.callback.onNodeCreated || !!setting.view.addDiyDom) {
                        var root = data.getRoot(setting);
                        root.createdNodes.push(node);
                    }
                },
                addZTreeTools: function (zTreeTools) {
                    _init.zTreeTools.push(zTreeTools);
                },
                exSetting: function (s) {
                    $.extend(true, _setting, s);
                },
                fixPIdKeyValue: function (setting, node) {
                    if (setting.data.simpleData.enable) {
                        node[setting.data.simpleData.pIdKey] = node.parentTId ? node.getParentNode()[setting.data.simpleData.idKey] : setting.data.simpleData.rootPId;
                    }
                },
                getAfterA: function (setting, node, array) {
                    for (var i = 0, j = _init.afterA.length; i < j; i++) {
                        _init.afterA[i].apply(this, arguments);
                    }
                },
                getBeforeA: function (setting, node, array) {
                    for (var i = 0, j = _init.beforeA.length; i < j; i++) {
                        _init.beforeA[i].apply(this, arguments);
                    }
                },
                getInnerAfterA: function (setting, node, array) {
                    for (var i = 0, j = _init.innerAfterA.length; i < j; i++) {
                        _init.innerAfterA[i].apply(this, arguments);
                    }
                },
                getInnerBeforeA: function (setting, node, array) {
                    for (var i = 0, j = _init.innerBeforeA.length; i < j; i++) {
                        _init.innerBeforeA[i].apply(this, arguments);
                    }
                },
                getCache: function (setting) {
                    return caches[setting.treeId];
                },
                getNodeIndex: function (setting, node) {
                    if (!node) return null;
                    var p = node.parentTId ? node.getParentNode() : data.getRoot(setting),
                        children = data.nodeChildren(setting, p);
                    for (var i = 0, l = children.length - 1; i <= l; i++) {
                        if (children[i] === node) {
                            return i;
                        }
                    }
                    return -1;
                },
                getNextNode: function (setting, node) {
                    if (!node) return null;
                    var p = node.parentTId ? node.getParentNode() : data.getRoot(setting),
                        children = data.nodeChildren(setting, p);
                    for (var i = 0, l = children.length - 1; i <= l; i++) {
                        if (children[i] === node) {
                            return (i == l ? null : children[i + 1]);
                        }
                    }
                    return null;
                },
                getNodeByParam: function (setting, nodes, key, value) {
                    if (!nodes || !key) return null;
                    for (var i = 0, l = nodes.length; i < l; i++) {
                        var node = nodes[i];
                        if (node[key] == value) {
                            return nodes[i];
                        }
                        var children = data.nodeChildren(setting, node);
                        var tmp = data.getNodeByParam(setting, children, key, value);
                        if (tmp) return tmp;
                    }
                    return null;
                },
                getNodeCache: function (setting, tId) {
                    if (!tId) return null;
                    var n = caches[setting.treeId].nodes[data.getNodeCacheId(tId)];
                    return n ? n : null;
                },
                getNodePath: function (setting, node) {
                    if (!node) return null;

                    var path;
                    if (node.parentTId) {
                        path = node.getParentNode().getPath();
                    } else {
                        path = [];
                    }

                    if (path) {
                        path.push(node);
                    }

                    return path;
                },
                getNodes: function (setting) {
                    return data.nodeChildren(setting, data.getRoot(setting));
                },
                getNodesByParam: function (setting, nodes, key, value) {
                    if (!nodes || !key) return [];
                    var result = [];
                    for (var i = 0, l = nodes.length; i < l; i++) {
                        var node = nodes[i];
                        if (node[key] == value) {
                            result.push(node);
                        }
                        var children = data.nodeChildren(setting, node);
                        result = result.concat(data.getNodesByParam(setting, children, key, value));
                    }
                    return result;
                },
                getNodesByParamFuzzy: function (setting, nodes, key, value) {
                    if (!nodes || !key) return [];
                    var result = [];
                    value = value.toLowerCase();
                    for (var i = 0, l = nodes.length; i < l; i++) {
                        var node = nodes[i];
                        if (typeof node[key] == "string" && nodes[i][key].toLowerCase().indexOf(value) > -1) {
                            result.push(node);
                        }
                        var children = data.nodeChildren(setting, node);
                        result = result.concat(data.getNodesByParamFuzzy(setting, children, key, value));
                    }
                    return result;
                },
                getNodesByFilter: function (setting, nodes, filter, isSingle, invokeParam) {
                    if (!nodes) return (isSingle ? null : []);
                    var result = isSingle ? null : [];
                    for (var i = 0, l = nodes.length; i < l; i++) {
                        var node = nodes[i];
                        if (tools.apply(filter, [node, invokeParam], false)) {
                            if (isSingle) {
                                return node;
                            }
                            result.push(node);
                        }
                        var children = data.nodeChildren(setting, node);
                        var tmpResult = data.getNodesByFilter(setting, children, filter, isSingle, invokeParam);
                        if (isSingle && !!tmpResult) {
                            return tmpResult;
                        }
                        result = isSingle ? tmpResult : result.concat(tmpResult);
                    }
                    return result;
                },
                getPreNode: function (setting, node) {
                    if (!node) return null;
                    var p = node.parentTId ? node.getParentNode() : data.getRoot(setting),
                        children = data.nodeChildren(setting, p);
                    for (var i = 0, l = children.length; i < l; i++) {
                        if (children[i] === node) {
                            return (i == 0 ? null : children[i - 1]);
                        }
                    }
                    return null;
                },
                getRoot: function (setting) {
                    return setting ? roots[setting.treeId] : null;
                },
                getRoots: function () {
                    return roots;
                },
                getSetting: function (treeId) {
                    return settings[treeId];
                },
                getSettings: function () {
                    return settings;
                },
                getZTreeTools: function (treeId) {
                    var r = this.getRoot(this.getSetting(treeId));
                    return r ? r.treeTools : null;
                },
                initCache: function (setting) {
                    for (var i = 0, j = _init.caches.length; i < j; i++) {
                        _init.caches[i].apply(this, arguments);
                    }
                },
                initNode: function (setting, level, node, parentNode, preNode, nextNode) {
                    for (var i = 0, j = _init.nodes.length; i < j; i++) {
                        _init.nodes[i].apply(this, arguments);
                    }
                },
                initRoot: function (setting) {
                    for (var i = 0, j = _init.roots.length; i < j; i++) {
                        _init.roots[i].apply(this, arguments);
                    }
                },
                isSelectedNode: function (setting, node) {
                    var root = data.getRoot(setting);
                    for (var i = 0, j = root.curSelectedList.length; i < j; i++) {
                        if (node === root.curSelectedList[i]) return true;
                    }
                    return false;
                },
                nodeChildren: function (setting, node, newChildren) {
                    if (!node) {
                        return null;
                    }
                    var key = setting.data.key.children;
                    if (typeof newChildren !== 'undefined') {
                        node[key] = newChildren;
                    }
                    return node[key];
                },
                nodeIsParent: function (setting, node, newIsParent) {
                    if (!node) {
                        return false;
                    }
                    var key = setting.data.key.isParent;
                    if (typeof newIsParent !== 'undefined') {
                        if (typeof newIsParent === "string") {
                            newIsParent = tools.eqs(newIsParent, "true");
                        }
                        newIsParent = !!newIsParent;
                        node[key] = newIsParent;
                    } else if (typeof node[key] == "string") {
                        node[key] = tools.eqs(node[key], "true");
                    } else {
                        node[key] = !!node[key];
                    }
                    return node[key];
                },
                nodeName: function (setting, node, newName) {
                    var key = setting.data.key.name;
                    if (typeof newName !== 'undefined') {
                        node[key] = newName;
                    }
                    return "" + node[key];
                },
                nodeTitle: function (setting, node) {
                    var t = setting.data.key.title === "" ? setting.data.key.name : setting.data.key.title;
                    return "" + node[t];
                },
                removeNodeCache: function (setting, node) {
                    var children = data.nodeChildren(setting, node);
                    if (children) {
                        for (var i = 0, l = children.length; i < l; i++) {
                            data.removeNodeCache(setting, children[i]);
                        }
                    }
                    data.getCache(setting).nodes[data.getNodeCacheId(node.tId)] = null;
                },
                removeSelectedNode: function (setting, node) {
                    var root = data.getRoot(setting);
                    for (var i = 0, j = root.curSelectedList.length; i < j; i++) {
                        if (node === root.curSelectedList[i] || !data.getNodeCache(setting, root.curSelectedList[i].tId)) {
                            root.curSelectedList.splice(i, 1);
                            setting.treeObj.trigger(consts.event.UNSELECTED, [setting.treeId, node]);
                            i--;
                            j--;
                        }
                    }
                },
                setCache: function (setting, cache) {
                    caches[setting.treeId] = cache;
                },
                setRoot: function (setting, root) {
                    roots[setting.treeId] = root;
                },
                setZTreeTools: function (setting, zTreeTools) {
                    for (var i = 0, j = _init.zTreeTools.length; i < j; i++) {
                        _init.zTreeTools[i].apply(this, arguments);
                    }
                },
                transformToArrayFormat: function (setting, nodes) {
                    if (!nodes) return [];
                    var r = [];
                    if (tools.isArray(nodes)) {
                        for (var i = 0, l = nodes.length; i < l; i++) {
                            var node = nodes[i];
                            _do(node);
                        }
                    } else {
                        _do(nodes);
                    }
                    return r;

                    function _do(_node) {
                        r.push(_node);
                        var children = data.nodeChildren(setting, _node);
                        if (children) {
                            r = r.concat(data.transformToArrayFormat(setting, children));
                        }
                    }
                },
                transformTozTreeFormat: function (setting, sNodes) {
                    var i, l,
                        key = setting.data.simpleData.idKey,
                        parentKey = setting.data.simpleData.pIdKey;
                    if (!key || key == "" || !sNodes) return [];

                    if (tools.isArray(sNodes)) {
                        var r = [];
                        var tmpMap = {};
                        for (i = 0, l = sNodes.length; i < l; i++) {
                            tmpMap[sNodes[i][key]] = sNodes[i];
                        }
                        for (i = 0, l = sNodes.length; i < l; i++) {
                            var p = tmpMap[sNodes[i][parentKey]];
                            if (p && sNodes[i][key] != sNodes[i][parentKey]) {
                                var children = data.nodeChildren(setting, p);
                                if (!children) {
                                    children = data.nodeChildren(setting, p, []);
                                }
                                children.push(sNodes[i]);
                            } else {
                                r.push(sNodes[i]);
                            }
                        }
                        return r;
                    } else {
                        return [sNodes];
                    }
                }
            },
            //method of event proxy
            event = {
                bindEvent: function (setting) {
                    for (var i = 0, j = _init.bind.length; i < j; i++) {
                        _init.bind[i].apply(this, arguments);
                    }
                },
                unbindEvent: function (setting) {
                    for (var i = 0, j = _init.unbind.length; i < j; i++) {
                        _init.unbind[i].apply(this, arguments);
                    }
                },
                bindTree: function (setting) {
                    var eventParam = {
                        treeId: setting.treeId
                    },
                        o = setting.treeObj;
                    if (!setting.view.txtSelectedEnable) {
                        // for can't select text
                        o.bind('selectstart', handler.onSelectStart).css({
                            "-moz-user-select": "-moz-none"
                        });
                    }
                    o.bind('click', eventParam, event.proxy);
                    o.bind('dblclick', eventParam, event.proxy);
                    o.bind('mouseover', eventParam, event.proxy);
                    o.bind('mouseout', eventParam, event.proxy);
                    o.bind('mousedown', eventParam, event.proxy);
                    o.bind('mouseup', eventParam, event.proxy);
                    o.bind('contextmenu', eventParam, event.proxy);
                },
                unbindTree: function (setting) {
                    var o = setting.treeObj;
                    o.unbind('selectstart', handler.onSelectStart)
                        .unbind('click', event.proxy)
                        .unbind('dblclick', event.proxy)
                        .unbind('mouseover', event.proxy)
                        .unbind('mouseout', event.proxy)
                        .unbind('mousedown', event.proxy)
                        .unbind('mouseup', event.proxy)
                        .unbind('contextmenu', event.proxy);
                },
                doProxy: function (e) {
                    var results = [];
                    for (var i = 0, j = _init.proxys.length; i < j; i++) {
                        var proxyResult = _init.proxys[i].apply(this, arguments);
                        results.push(proxyResult);
                        if (proxyResult.stop) {
                            break;
                        }
                    }
                    return results;
                },
                proxy: function (e) {
                    var setting = data.getSetting(e.data.treeId);
                    if (!tools.uCanDo(setting, e)) return true;
                    var results = event.doProxy(e),
                        r = true, x = false;
                    for (var i = 0, l = results.length; i < l; i++) {
                        var proxyResult = results[i];
                        if (proxyResult.nodeEventCallback) {
                            x = true;
                            r = proxyResult.nodeEventCallback.apply(proxyResult, [e, proxyResult.node]) && r;
                        }
                        if (proxyResult.treeEventCallback) {
                            x = true;
                            r = proxyResult.treeEventCallback.apply(proxyResult, [e, proxyResult.node]) && r;
                        }
                    }
                    return r;
                }
            },
            //method of event handler
            handler = {
                onSwitchNode: function (event, node) {
                    var setting = data.getSetting(event.data.treeId);
                    if (node.open) {
                        if (tools.apply(setting.callback.beforeCollapse, [setting.treeId, node], true) == false) return true;
                        data.getRoot(setting).expandTriggerFlag = true;
                        view.switchNode(setting, node);
                    } else {
                        if (tools.apply(setting.callback.beforeExpand, [setting.treeId, node], true) == false) return true;
                        data.getRoot(setting).expandTriggerFlag = true;
                        view.switchNode(setting, node);
                    }
                    return true;
                },
                onClickNode: function (event, node) {
                    var setting = data.getSetting(event.data.treeId),
                        clickFlag = ((setting.view.autoCancelSelected && (event.ctrlKey || event.metaKey)) && data.isSelectedNode(setting, node)) ? 0 : (setting.view.autoCancelSelected && (event.ctrlKey || event.metaKey) && setting.view.selectedMulti) ? 2 : 1;
                    if (tools.apply(setting.callback.beforeClick, [setting.treeId, node, clickFlag], true) == false) return true;
                    if (clickFlag === 0) {
                        view.cancelPreSelectedNode(setting, node);
                    } else {
                        view.selectNode(setting, node, clickFlag === 2);
                    }
                    setting.treeObj.trigger(consts.event.CLICK, [event, setting.treeId, node, clickFlag]);
                    return true;
                },
                onZTreeMousedown: function (event, node) {
                    var setting = data.getSetting(event.data.treeId);
                    if (tools.apply(setting.callback.beforeMouseDown, [setting.treeId, node], true)) {
                        tools.apply(setting.callback.onMouseDown, [event, setting.treeId, node]);
                    }
                    return true;
                },
                onZTreeMouseup: function (event, node) {
                    var setting = data.getSetting(event.data.treeId);
                    if (tools.apply(setting.callback.beforeMouseUp, [setting.treeId, node], true)) {
                        tools.apply(setting.callback.onMouseUp, [event, setting.treeId, node]);
                    }
                    return true;
                },
                onZTreeDblclick: function (event, node) {
                    var setting = data.getSetting(event.data.treeId);
                    if (tools.apply(setting.callback.beforeDblClick, [setting.treeId, node], true)) {
                        tools.apply(setting.callback.onDblClick, [event, setting.treeId, node]);
                    }
                    return true;
                },
                onZTreeContextmenu: function (event, node) {
                    var setting = data.getSetting(event.data.treeId);
                    if (tools.apply(setting.callback.beforeRightClick, [setting.treeId, node], true)) {
                        tools.apply(setting.callback.onRightClick, [event, setting.treeId, node]);
                    }
                    return (typeof setting.callback.onRightClick) != "function";
                },
                onSelectStart: function (e) {
                    var n = e.originalEvent.srcElement.nodeName.toLowerCase();
                    return (n === "input" || n === "textarea");
                }
            },
            //method of tools for zTree
            tools = {
                apply: function (fun, param, defaultValue) {
                    if ((typeof fun) == "function") {
                        return fun.apply(zt, param ? param : []);
                    }
                    return defaultValue;
                },
                canAsync: function (setting, node) {
                    var children = data.nodeChildren(setting, node);
                    var isParent = data.nodeIsParent(setting, node);
                    return (setting.async.enable && node && isParent && !(node.zAsync || (children && children.length > 0)));
                },
                clone: function (obj) {
                    if (obj === null) return null;
                    var o = tools.isArray(obj) ? [] : {};
                    for (var i in obj) {
                        o[i] = (obj[i] instanceof Date) ? new Date(obj[i].getTime()) : (typeof obj[i] === "object" ? tools.clone(obj[i]) : obj[i]);
                    }
                    return o;
                },
                eqs: function (str1, str2) {
                    return str1.toLowerCase() === str2.toLowerCase();
                },
                isArray: function (arr) {
                    return Object.prototype.toString.apply(arr) === "[object Array]";
                },
                isElement: function (o) {
                    return (
                        typeof HTMLElement === "object" ? o instanceof HTMLElement : //DOM2
                            o && typeof o === "object" && o !== null && o.nodeType === 1 && typeof o.nodeName === "string"
                    );
                },
                $: function (node, exp, setting) {
                    if (!!exp && typeof exp != "string") {
                        setting = exp;
                        exp = "";
                    }
                    if (typeof node == "string") {
                        return $(node, setting ? setting.treeObj.get(0).ownerDocument : null);
                    } else {
                        return $("#" + node.tId + exp, setting ? setting.treeObj : null);
                    }
                },
                getMDom: function (setting, curDom, targetExpr) {
                    if (!curDom) return null;
                    while (curDom && curDom.id !== setting.treeId) {
                        for (var i = 0, l = targetExpr.length; curDom.tagName && i < l; i++) {
                            if (tools.eqs(curDom.tagName, targetExpr[i].tagName) && curDom.getAttribute(targetExpr[i].attrName) !== null) {
                                return curDom;
                            }
                        }
                        curDom = curDom.parentNode;
                    }
                    return null;
                },
                getNodeMainDom: function (target) {
                    return ($(target).parent("li").get(0) || $(target).parentsUntil("li").parent().get(0));
                },
                isChildOrSelf: function (dom, parentId) {
                    return ($(dom).closest("#" + parentId).length > 0);
                },
                uCanDo: function (setting, e) {
                    return true;
                }
            },
            //method of operate ztree dom
            view = {
                addNodes: function (setting, parentNode, index, newNodes, isSilent) {
                    var isParent = data.nodeIsParent(setting, parentNode);
                    if (setting.data.keep.leaf && parentNode && !isParent) {
                        return;
                    }
                    if (!tools.isArray(newNodes)) {
                        newNodes = [newNodes];
                    }
                    if (setting.data.simpleData.enable) {
                        newNodes = data.transformTozTreeFormat(setting, newNodes);
                    }
                    if (parentNode) {
                        var target_switchObj = $$(parentNode, consts.id.SWITCH, setting),
                            target_icoObj = $$(parentNode, consts.id.ICON, setting),
                            target_ulObj = $$(parentNode, consts.id.UL, setting);

                        if (!parentNode.open) {
                            view.replaceSwitchClass(parentNode, target_switchObj, consts.folder.CLOSE);
                            view.replaceIcoClass(parentNode, target_icoObj, consts.folder.CLOSE);
                            parentNode.open = false;
                            target_ulObj.css({
                                "display": "none"
                            });
                        }

                        data.addNodesData(setting, parentNode, index, newNodes);
                        view.createNodes(setting, parentNode.level + 1, newNodes, parentNode, index);
                        if (!isSilent) {
                            view.expandCollapseParentNode(setting, parentNode, true);
                        }
                    } else {
                        data.addNodesData(setting, data.getRoot(setting), index, newNodes);
                        view.createNodes(setting, 0, newNodes, null, index);
                    }
                },
                appendNodes: function (setting, level, nodes, parentNode, index, initFlag, openFlag) {
                    if (!nodes) return [];
                    var html = [];

                    var tmpPNode = (parentNode) ? parentNode : data.getRoot(setting),
                        tmpPChild = data.nodeChildren(setting, tmpPNode),
                        isFirstNode, isLastNode;

                    if (!tmpPChild || index >= tmpPChild.length - nodes.length) {
                        index = -1;
                    }

                    for (var i = 0, l = nodes.length; i < l; i++) {
                        var node = nodes[i];
                        if (initFlag) {
                            isFirstNode = ((index === 0 || tmpPChild.length == nodes.length) && (i == 0));
                            isLastNode = (index < 0 && i == (nodes.length - 1));
                            data.initNode(setting, level, node, parentNode, isFirstNode, isLastNode, openFlag);
                            data.addNodeCache(setting, node);
                        }
                        var isParent = data.nodeIsParent(setting, node);

                        var childHtml = [];
                        var children = data.nodeChildren(setting, node);
                        if (children && children.length > 0) {
                            //make child html first, because checkType
                            childHtml = view.appendNodes(setting, level + 1, children, node, -1, initFlag, openFlag && node.open);
                        }
                        if (openFlag) {
                            view.makeDOMNodeMainBefore(html, setting, node);
                            view.makeDOMNodeLine(html, setting, node);
                            data.getBeforeA(setting, node, html);
                            view.makeDOMNodeNameBefore(html, setting, node);
                            data.getInnerBeforeA(setting, node, html);
                            view.makeDOMNodeIcon(html, setting, node);
                            data.getInnerAfterA(setting, node, html);
                            view.makeDOMNodeNameAfter(html, setting, node);
                            data.getAfterA(setting, node, html);
                            if (isParent && node.open) {
                                view.makeUlHtml(setting, node, html, childHtml.join(''));
                            }
                            view.makeDOMNodeMainAfter(html, setting, node);
                            data.addCreatedNode(setting, node);
                        }
                    }
                    return html;
                },
                appendParentULDom: function (setting, node) {
                    var html = [],
                        nObj = $$(node, setting);
                    if (!nObj.get(0) && !!node.parentTId) {
                        view.appendParentULDom(setting, node.getParentNode());
                        nObj = $$(node, setting);
                    }
                    var ulObj = $$(node, consts.id.UL, setting);
                    if (ulObj.get(0)) {
                        ulObj.remove();
                    }
                    var children = data.nodeChildren(setting, node),
                        childHtml = view.appendNodes(setting, node.level + 1, children, node, -1, false, true);
                    view.makeUlHtml(setting, node, html, childHtml.join(''));
                    nObj.append(html.join(''));
                },
                asyncNode: function (setting, node, isSilent, callback) {
                    var i, l;
                    var isParent = data.nodeIsParent(setting, node);
                    if (node && !isParent) {
                        tools.apply(callback);
                        return false;
                    } else if (node && node.isAjaxing) {
                        return false;
                    } else if (tools.apply(setting.callback.beforeAsync, [setting.treeId, node], true) == false) {
                        tools.apply(callback);
                        return false;
                    }
                    if (node) {
                        node.isAjaxing = true;
                        var icoObj = $$(node, consts.id.ICON, setting);
                        icoObj.attr({ "style": "", "class": consts.className.BUTTON + " " + consts.className.ICO_LOADING });
                    }

                    var tmpParam = {};
                    var autoParam = tools.apply(setting.async.autoParam, [setting.treeId, node], setting.async.autoParam);
                    for (i = 0, l = autoParam.length; node && i < l; i++) {
                        var pKey = autoParam[i].split("="), spKey = pKey;
                        if (pKey.length > 1) {
                            spKey = pKey[1];
                            pKey = pKey[0];
                        }
                        tmpParam[spKey] = node[pKey];
                    }
                    var otherParam = tools.apply(setting.async.otherParam, [setting.treeId, node], setting.async.otherParam);
                    if (tools.isArray(otherParam)) {
                        for (i = 0, l = otherParam.length; i < l; i += 2) {
                            tmpParam[otherParam[i]] = otherParam[i + 1];
                        }
                    } else {
                        for (var p in otherParam) {
                            tmpParam[p] = otherParam[p];
                        }
                    }
                    if (setting.async.useRsa) {
                        tmpParam = $.encrypt(tmpParam);
                    }

                    var _tmpV = data.getRoot(setting)._ver;
                    var $url=tools.apply(setting.async.url, [setting.treeId, node], setting.async.url);
                    $.ajax({
                        contentType: setting.async.contentType,
                        cache: false,
                        type: setting.async.type,
                        url: $url,
                        data: setting.async.contentType.indexOf('application/json') > -1 ? JSON.stringify(tmpParam) : tmpParam,
                        dataType: setting.async.dataType,
                        headers: setting.async.headers,
                        xhrFields: setting.async.xhrFields,
                        success: function (msg) {
                            if (_tmpV != data.getRoot(setting)._ver) {
                                return;
                            }

                            if (msg != undefined && msg.data != undefined && typeof msg.data === 'object' && msg.data.constructor === Array) {
                                msg = msg.data;
                            }
                            if (msg.ciphertext != undefined && msg.ciphertext.length > 2) {
                                msg = $.decrypt(msg.ciphertext, $url);
                            }

                            var newNodes = [];
                            try {
                                if (!msg || msg.length == 0) {
                                    newNodes = [];
                                } else if (typeof msg == "string") {
                                    newNodes = eval("(" + msg + ")");
                                } else {
                                    newNodes = msg;
                                }
                            } catch (err) {
                                newNodes = msg;
                            }

                            if (node) {
                                node.isAjaxing = null;
                                node.zAsync = true;
                            }
                            view.setNodeLineIcos(setting, node);
                            if (newNodes && newNodes !== "") {
                                newNodes = tools.apply(setting.async.dataFilter, [setting.treeId, node, newNodes], newNodes);
                                view.addNodes(setting, node, -1, !!newNodes ? tools.clone(newNodes) : [], !!isSilent);
                            } else {
                                view.addNodes(setting, node, -1, [], !!isSilent);
                            }
                            setting.treeObj.trigger(consts.event.ASYNC_SUCCESS, [setting.treeId, node, msg]);
                            tools.apply(callback);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if (_tmpV != data.getRoot(setting)._ver) {
                                return;
                            }
                            if (node) node.isAjaxing = null;
                            view.setNodeLineIcos(setting, node);
                            setting.treeObj.trigger(consts.event.ASYNC_ERROR, [setting.treeId, node, XMLHttpRequest, textStatus, errorThrown]);
                        }
                    });
                    return true;
                },
                cancelPreSelectedNode: function (setting, node, excludeNode) {
                    var list = data.getRoot(setting).curSelectedList,
                        i, n;
                    for (i = list.length - 1; i >= 0; i--) {
                        n = list[i];
                        if (node === n || (!node && (!excludeNode || excludeNode !== n))) {
                            $$(n, consts.id.A, setting).removeClass(consts.node.CURSELECTED);
                            if (node) {
                                data.removeSelectedNode(setting, node);
                                break;
                            } else {
                                list.splice(i, 1);
                                setting.treeObj.trigger(consts.event.UNSELECTED, [setting.treeId, n]);
                            }
                        }
                    }
                },
                createNodeCallback: function (setting) {
                    if (!!setting.callback.onNodeCreated || !!setting.view.addDiyDom) {
                        var root = data.getRoot(setting);
                        while (root.createdNodes.length > 0) {
                            var node = root.createdNodes.shift();
                            tools.apply(setting.view.addDiyDom, [setting.treeId, node]);
                            if (!!setting.callback.onNodeCreated) {
                                setting.treeObj.trigger(consts.event.NODECREATED, [setting.treeId, node]);
                            }
                        }
                    }
                },
                createNodes: function (setting, level, nodes, parentNode, index) {
                    if (!nodes || nodes.length == 0) return;
                    var root = data.getRoot(setting),
                        openFlag = !parentNode || parentNode.open || !!$$(data.nodeChildren(setting, parentNode)[0], setting).get(0);
                    root.createdNodes = [];
                    var zTreeHtml = view.appendNodes(setting, level, nodes, parentNode, index, true, openFlag),
                        parentObj, nextObj;

                    if (!parentNode) {
                        parentObj = setting.treeObj;
                        //setting.treeObj.append(zTreeHtml.join(''));
                    } else {
                        var ulObj = $$(parentNode, consts.id.UL, setting);
                        if (ulObj.get(0)) {
                            parentObj = ulObj;
                            //ulObj.append(zTreeHtml.join(''));
                        }
                    }
                    if (parentObj) {
                        if (index >= 0) {
                            nextObj = parentObj.children()[index];
                        }
                        if (index >= 0 && nextObj) {
                            $(nextObj).before(zTreeHtml.join(''));
                        } else {
                            parentObj.append(zTreeHtml.join(''));
                        }
                    }

                    view.createNodeCallback(setting);
                },
                destroy: function (setting) {
                    if (!setting) return;
                    data.initCache(setting);
                    data.initRoot(setting);
                    event.unbindTree(setting);
                    event.unbindEvent(setting);
                    setting.treeObj.empty();
                    delete settings[setting.treeId];
                },
                expandCollapseNode: function (setting, node, expandFlag, animateFlag, callback) {
                    var root = data.getRoot(setting);
                    var tmpCb, _callback;
                    if (!node) {
                        tools.apply(callback, []);
                        return;
                    }
                    var children = data.nodeChildren(setting, node);
                    var isParent = data.nodeIsParent(setting, node);
                    if (root.expandTriggerFlag) {
                        _callback = callback;
                        tmpCb = function () {
                            if (_callback) _callback();
                            if (node.open) {
                                setting.treeObj.trigger(consts.event.EXPAND, [setting.treeId, node]);
                            } else {
                                setting.treeObj.trigger(consts.event.COLLAPSE, [setting.treeId, node]);
                            }
                        };
                        callback = tmpCb;
                        root.expandTriggerFlag = false;
                    }
                    if (!node.open && isParent && ((!$$(node, consts.id.UL, setting).get(0)) || (children && children.length > 0 && !$$(children[0], setting).get(0)))) {
                        view.appendParentULDom(setting, node);
                        view.createNodeCallback(setting);
                    }
                    if (node.open == expandFlag) {
                        tools.apply(callback, []);
                        return;
                    }
                    var ulObj = $$(node, consts.id.UL, setting),
                        switchObj = $$(node, consts.id.SWITCH, setting),
                        icoObj = $$(node, consts.id.ICON, setting);

                    if (isParent) {
                        node.open = !node.open;
                        if (node.iconOpen && node.iconClose) {
                            icoObj.attr("style", view.makeNodeIcoStyle(setting, node));
                        }

                        if (node.open) {
                            view.replaceSwitchClass(node, switchObj, consts.folder.OPEN);
                            view.replaceIcoClass(node, icoObj, consts.folder.OPEN);
                            if (animateFlag == false || setting.view.expandSpeed == "") {
                                ulObj.show();
                                tools.apply(callback, []);
                            } else {
                                if (children && children.length > 0) {
                                    ulObj.slideDown(setting.view.expandSpeed, callback);
                                } else {
                                    ulObj.show();
                                    tools.apply(callback, []);
                                }
                            }
                        } else {
                            view.replaceSwitchClass(node, switchObj, consts.folder.CLOSE);
                            view.replaceIcoClass(node, icoObj, consts.folder.CLOSE);
                            if (animateFlag == false || setting.view.expandSpeed == "" || !(children && children.length > 0)) {
                                ulObj.hide();
                                tools.apply(callback, []);
                            } else {
                                ulObj.slideUp(setting.view.expandSpeed, callback);
                            }
                        }
                    } else {
                        tools.apply(callback, []);
                    }
                },
                expandCollapseParentNode: function (setting, node, expandFlag, animateFlag, callback) {
                    if (!node) return;
                    if (!node.parentTId) {
                        view.expandCollapseNode(setting, node, expandFlag, animateFlag, callback);
                        return;
                    } else {
                        view.expandCollapseNode(setting, node, expandFlag, animateFlag);
                    }
                    if (node.parentTId) {
                        view.expandCollapseParentNode(setting, node.getParentNode(), expandFlag, animateFlag, callback);
                    }
                },
                expandCollapseSonNode: function (setting, node, expandFlag, animateFlag, callback) {
                    var root = data.getRoot(setting),
                        treeNodes = (node) ? data.nodeChildren(setting, node) : data.nodeChildren(setting, root),
                        selfAnimateSign = (node) ? false : animateFlag,
                        expandTriggerFlag = data.getRoot(setting).expandTriggerFlag;
                    data.getRoot(setting).expandTriggerFlag = false;
                    if (treeNodes) {
                        for (var i = 0, l = treeNodes.length; i < l; i++) {
                            if (treeNodes[i]) view.expandCollapseSonNode(setting, treeNodes[i], expandFlag, selfAnimateSign);
                        }
                    }
                    data.getRoot(setting).expandTriggerFlag = expandTriggerFlag;
                    view.expandCollapseNode(setting, node, expandFlag, animateFlag, callback);
                },
                isSelectedNode: function (setting, node) {
                    if (!node) {
                        return false;
                    }
                    var list = data.getRoot(setting).curSelectedList,
                        i;
                    for (i = list.length - 1; i >= 0; i--) {
                        if (node === list[i]) {
                            return true;
                        }
                    }
                    return false;
                },
                makeDOMNodeIcon: function (html, setting, node) {
                    var nameStr = data.nodeName(setting, node),
                        name = setting.view.nameIsHTML ? nameStr : nameStr.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    html.push("<span id='", node.tId, consts.id.ICON,
                        "' title='' treeNode", consts.id.ICON, " class='", view.makeNodeIcoClass(setting, node),
                        "' style='", view.makeNodeIcoStyle(setting, node), "'></span><span id='", node.tId, consts.id.SPAN,
                        "' class='", consts.className.NAME,
                        "'>", name, "</span>");
                },
                makeDOMNodeLine: function (html, setting, node) {
                    html.push("<span id='", node.tId, consts.id.SWITCH, "' title='' class='", view.makeNodeLineClass(setting, node), "' treeNode", consts.id.SWITCH, "></span>");
                },
                makeDOMNodeMainAfter: function (html, setting, node) {
                    html.push("</li>");
                },
                makeDOMNodeMainBefore: function (html, setting, node) {
                    html.push("<li id='", node.tId, "' class='", consts.className.LEVEL, node.level, "' tabindex='0' hidefocus='true' treenode>");
                },
                makeDOMNodeNameAfter: function (html, setting, node) {
                    html.push("</a>");
                },
                makeDOMNodeNameBefore: function (html, setting, node) {
                    var title = data.nodeTitle(setting, node),
                        url = view.makeNodeUrl(setting, node),
                        fontcss = view.makeNodeFontCss(setting, node),
                        fontStyle = [];
                    for (var f in fontcss) {
                        fontStyle.push(f, ":", fontcss[f], ";");
                    }
                    html.push("<a id='", node.tId, consts.id.A, "' class='", consts.className.LEVEL, node.level, "' treeNode", consts.id.A, " onclick=\"", (node.click || ''),
                        "\" ", ((url != null && url.length > 0) ? "href='" + url + "'" : ""), " target='", view.makeNodeTarget(node), "' style='", fontStyle.join(''),
                        "'");
                    if (tools.apply(setting.view.showTitle, [setting.treeId, node], setting.view.showTitle) && title) {
                        html.push("title='", title.replace(/'/g, "&#39;").replace(/</g, '&lt;').replace(/>/g, '&gt;'), "'");
                    }
                    html.push(">");
                },
                makeNodeFontCss: function (setting, node) {
                    var fontCss = tools.apply(setting.view.fontCss, [setting.treeId, node], setting.view.fontCss);
                    return (fontCss && ((typeof fontCss) != "function")) ? fontCss : {};
                },
                makeNodeIcoClass: function (setting, node) {
                    var icoCss = ["ico"];
                    if (!node.isAjaxing) {
                        var isParent = data.nodeIsParent(setting, node);
                        icoCss[0] = (node.iconSkin ? node.iconSkin + "_" : "") + icoCss[0];
                        if (isParent) {
                            icoCss.push(node.open ? consts.folder.OPEN : consts.folder.CLOSE);
                        } else {
                            icoCss.push(consts.folder.DOCU);
                        }
                    }
                    return consts.className.BUTTON + " " + icoCss.join('_');
                },
                makeNodeIcoStyle: function (setting, node) {
                    var icoStyle = [];
                    if (!node.isAjaxing) {
                        var isParent = data.nodeIsParent(setting, node);
                        var icon = (isParent && node.iconOpen && node.iconClose) ? (node.open ? node.iconOpen : node.iconClose) : node[setting.data.key.icon];
                        if (icon) icoStyle.push("background:url(", icon, ") 0 0 no-repeat;");
                        if (setting.view.showIcon == false || !tools.apply(setting.view.showIcon, [setting.treeId, node], true)) {
                            icoStyle.push("width:0px;height:0px;");
                        }
                    }
                    return icoStyle.join('');
                },
                makeNodeLineClass: function (setting, node) {
                    var lineClass = [];
                    if (setting.view.showLine) {
                        if (node.level == 0 && node.isFirstNode && node.isLastNode) {
                            lineClass.push(consts.line.ROOT);
                        } else if (node.level == 0 && node.isFirstNode) {
                            lineClass.push(consts.line.ROOTS);
                        } else if (node.isLastNode) {
                            lineClass.push(consts.line.BOTTOM);
                        } else {
                            lineClass.push(consts.line.CENTER);
                        }
                    } else {
                        lineClass.push(consts.line.NOLINE);
                    }
                    if (data.nodeIsParent(setting, node)) {
                        lineClass.push(node.open ? consts.folder.OPEN : consts.folder.CLOSE);
                    } else {
                        lineClass.push(consts.folder.DOCU);
                    }
                    return view.makeNodeLineClassEx(node) + lineClass.join('_');
                },
                makeNodeLineClassEx: function (node) {
                    return consts.className.BUTTON + " " + consts.className.LEVEL + node.level + " " + consts.className.SWITCH + " ";
                },
                makeNodeTarget: function (node) {
                    return (node.target || "_blank");
                },
                makeNodeUrl: function (setting, node) {
                    var urlKey = setting.data.key.url;
                    return node[urlKey] ? node[urlKey] : null;
                },
                makeUlHtml: function (setting, node, html, content) {
                    html.push("<ul id='", node.tId, consts.id.UL, "' class='", consts.className.LEVEL, node.level, " ", view.makeUlLineClass(setting, node), "' style='display:", (node.open ? "block" : "none"), "'>");
                    html.push(content);
                    html.push("</ul>");
                },
                makeUlLineClass: function (setting, node) {
                    return ((setting.view.showLine && !node.isLastNode) ? consts.line.LINE : "");
                },
                removeChildNodes: function (setting, node) {
                    if (!node) return;
                    var nodes = data.nodeChildren(setting, node);
                    if (!nodes) return;

                    for (var i = 0, l = nodes.length; i < l; i++) {
                        data.removeNodeCache(setting, nodes[i]);
                    }
                    data.removeSelectedNode(setting);
                    delete node[setting.data.key.children];

                    if (!setting.data.keep.parent) {
                        data.nodeIsParent(setting, node, false);
                        node.open = false;
                        var tmp_switchObj = $$(node, consts.id.SWITCH, setting),
                            tmp_icoObj = $$(node, consts.id.ICON, setting);
                        view.replaceSwitchClass(node, tmp_switchObj, consts.folder.DOCU);
                        view.replaceIcoClass(node, tmp_icoObj, consts.folder.DOCU);
                        $$(node, consts.id.UL, setting).remove();
                    } else {
                        $$(node, consts.id.UL, setting).empty();
                    }
                },
                scrollIntoView: function (setting, dom) {
                    if (!dom) {
                        return;
                    }
                    // support IE 7
                    if (typeof Element === 'undefined') {
                        var contRect = setting.treeObj.get(0).getBoundingClientRect(),
                            findMeRect = dom.getBoundingClientRect();
                        if (findMeRect.top < contRect.top || findMeRect.bottom > contRect.bottom
                            || findMeRect.right > contRect.right || findMeRect.left < contRect.left) {
                            dom.scrollIntoView();
                        }
                        return;
                    }
                    // CC-BY jocki84@googlemail.com, https://gist.github.com/jocki84/6ffafd003387179a988e
                    if (!Element.prototype.scrollIntoViewIfNeeded) {
                        Element.prototype.scrollIntoViewIfNeeded = function (centerIfNeeded) {
                            "use strict";

                            function makeRange(start, length) {
                                return { "start": start, "length": length, "end": start + length };
                            }

                            function coverRange(inner, outer) {
                                if (
                                    false === centerIfNeeded ||
                                    (outer.start < inner.end && inner.start < outer.end)
                                ) {
                                    return Math.max(
                                        inner.end - outer.length,
                                        Math.min(outer.start, inner.start)
                                    );
                                }
                                return (inner.start + inner.end - outer.length) / 2;
                            }

                            function makePoint(x, y) {
                                return {
                                    "x": x,
                                    "y": y,
                                    "translate": function translate(dX, dY) {
                                        return makePoint(x + dX, y + dY);
                                    }
                                };
                            }

                            function absolute(elem, pt) {
                                while (elem) {
                                    pt = pt.translate(elem.offsetLeft, elem.offsetTop);
                                    elem = elem.offsetParent;
                                }
                                return pt;
                            }

                            var target = absolute(this, makePoint(0, 0)),
                                extent = makePoint(this.offsetWidth, this.offsetHeight),
                                elem = this.parentNode,
                                origin;

                            while (elem instanceof HTMLElement) {
                                // Apply desired scroll amount.
                                origin = absolute(elem, makePoint(elem.clientLeft, elem.clientTop));
                                elem.scrollLeft = coverRange(
                                    makeRange(target.x - origin.x, extent.x),
                                    makeRange(elem.scrollLeft, elem.clientWidth)
                                );
                                elem.scrollTop = coverRange(
                                    makeRange(target.y - origin.y, extent.y),
                                    makeRange(elem.scrollTop, elem.clientHeight)
                                );

                                // Determine actual scroll amount by reading back scroll properties.
                                target = target.translate(-elem.scrollLeft, -elem.scrollTop);
                                elem = elem.parentNode;
                            }
                        };
                    }
                    dom.scrollIntoViewIfNeeded();
                },
                setFirstNode: function (setting, parentNode) {
                    var children = data.nodeChildren(setting, parentNode);
                    if (children.length > 0) {
                        children[0].isFirstNode = true;
                    }
                },
                setLastNode: function (setting, parentNode) {
                    var children = data.nodeChildren(setting, parentNode);
                    if (children.length > 0) {
                        children[children.length - 1].isLastNode = true;
                    }
                },
                removeNode: function (setting, node) {
                    var root = data.getRoot(setting),
                        parentNode = (node.parentTId) ? node.getParentNode() : root;

                    node.isFirstNode = false;
                    node.isLastNode = false;
                    node.getPreNode = function () {
                        return null;
                    };
                    node.getNextNode = function () {
                        return null;
                    };

                    if (!data.getNodeCache(setting, node.tId)) {
                        return;
                    }

                    $$(node, setting).remove();
                    data.removeNodeCache(setting, node);
                    data.removeSelectedNode(setting, node);

                    var children = data.nodeChildren(setting, parentNode);
                    for (var i = 0, l = children.length; i < l; i++) {
                        if (children[i].tId == node.tId) {
                            children.splice(i, 1);
                            break;
                        }
                    }
                    view.setFirstNode(setting, parentNode);
                    view.setLastNode(setting, parentNode);

                    var tmp_ulObj, tmp_switchObj, tmp_icoObj,
                        childLength = children.length;

                    //repair nodes old parent
                    if (!setting.data.keep.parent && childLength == 0) {
                        //old parentNode has no child nodes
                        data.nodeIsParent(setting, parentNode, false);
                        parentNode.open = false;
                        delete parentNode[setting.data.key.children];
                        tmp_ulObj = $$(parentNode, consts.id.UL, setting);
                        tmp_switchObj = $$(parentNode, consts.id.SWITCH, setting);
                        tmp_icoObj = $$(parentNode, consts.id.ICON, setting);
                        view.replaceSwitchClass(parentNode, tmp_switchObj, consts.folder.DOCU);
                        view.replaceIcoClass(parentNode, tmp_icoObj, consts.folder.DOCU);
                        tmp_ulObj.css("display", "none");

                    } else if (setting.view.showLine && childLength > 0) {
                        //old parentNode has child nodes
                        var newLast = children[childLength - 1];
                        tmp_ulObj = $$(newLast, consts.id.UL, setting);
                        tmp_switchObj = $$(newLast, consts.id.SWITCH, setting);
                        tmp_icoObj = $$(newLast, consts.id.ICON, setting);
                        if (parentNode == root) {
                            if (children.length == 1) {
                                //node was root, and ztree has only one root after move node
                                view.replaceSwitchClass(newLast, tmp_switchObj, consts.line.ROOT);
                            } else {
                                var tmp_first_switchObj = $$(children[0], consts.id.SWITCH, setting);
                                view.replaceSwitchClass(children[0], tmp_first_switchObj, consts.line.ROOTS);
                                view.replaceSwitchClass(newLast, tmp_switchObj, consts.line.BOTTOM);
                            }
                        } else {
                            view.replaceSwitchClass(newLast, tmp_switchObj, consts.line.BOTTOM);
                        }
                        tmp_ulObj.removeClass(consts.line.LINE);
                    }
                },
                replaceIcoClass: function (node, obj, newName) {
                    if (!obj || node.isAjaxing) return;
                    var tmpName = obj.attr("class");
                    if (tmpName == undefined) return;
                    var tmpList = tmpName.split("_");
                    switch (newName) {
                        case consts.folder.OPEN:
                        case consts.folder.CLOSE:
                        case consts.folder.DOCU:
                            tmpList[tmpList.length - 1] = newName;
                            break;
                    }
                    obj.attr("class", tmpList.join("_"));
                },
                replaceSwitchClass: function (node, obj, newName) {
                    if (!obj) return;
                    var tmpName = obj.attr("class");
                    if (tmpName == undefined) return;
                    var tmpList = tmpName.split("_");
                    switch (newName) {
                        case consts.line.ROOT:
                        case consts.line.ROOTS:
                        case consts.line.CENTER:
                        case consts.line.BOTTOM:
                        case consts.line.NOLINE:
                            tmpList[0] = view.makeNodeLineClassEx(node) + newName;
                            break;
                        case consts.folder.OPEN:
                        case consts.folder.CLOSE:
                        case consts.folder.DOCU:
                            tmpList[1] = newName;
                            break;
                    }
                    obj.attr("class", tmpList.join("_"));
                    if (newName !== consts.folder.DOCU) {
                        obj.removeAttr("disabled");
                    } else {
                        obj.attr("disabled", "disabled");
                    }
                },
                selectNode: function (setting, node, addFlag) {
                    if (!addFlag) {
                        view.cancelPreSelectedNode(setting, null, node);
                    }
                    $$(node, consts.id.A, setting).addClass(consts.node.CURSELECTED);
                    data.addSelectedNode(setting, node);
                    setting.treeObj.trigger(consts.event.SELECTED, [setting.treeId, node]);
                },
                setNodeFontCss: function (setting, treeNode) {
                    var aObj = $$(treeNode, consts.id.A, setting),
                        fontCss = view.makeNodeFontCss(setting, treeNode);
                    if (fontCss) {
                        aObj.css(fontCss);
                    }
                },
                setNodeLineIcos: function (setting, node) {
                    if (!node) return;
                    var switchObj = $$(node, consts.id.SWITCH, setting),
                        ulObj = $$(node, consts.id.UL, setting),
                        icoObj = $$(node, consts.id.ICON, setting),
                        ulLine = view.makeUlLineClass(setting, node);
                    if (ulLine.length == 0) {
                        ulObj.removeClass(consts.line.LINE);
                    } else {
                        ulObj.addClass(ulLine);
                    }
                    switchObj.attr("class", view.makeNodeLineClass(setting, node));
                    if (data.nodeIsParent(setting, node)) {
                        switchObj.removeAttr("disabled");
                    } else {
                        switchObj.attr("disabled", "disabled");
                    }
                    icoObj.removeAttr("style");
                    icoObj.attr("style", view.makeNodeIcoStyle(setting, node));
                    icoObj.attr("class", view.makeNodeIcoClass(setting, node));
                },
                setNodeName: function (setting, node) {
                    var title = data.nodeTitle(setting, node),
                        nObj = $$(node, consts.id.SPAN, setting);
                    nObj.empty();
                    if (setting.view.nameIsHTML) {
                        nObj.html(data.nodeName(setting, node));
                    } else {
                        nObj.text(data.nodeName(setting, node));
                    }
                    if (tools.apply(setting.view.showTitle, [setting.treeId, node], setting.view.showTitle)) {
                        var aObj = $$(node, consts.id.A, setting);
                        aObj.attr("title", !title ? "" : title);
                    }
                },
                setNodeTarget: function (setting, node) {
                    var aObj = $$(node, consts.id.A, setting);
                    aObj.attr("target", view.makeNodeTarget(node));
                },
                setNodeUrl: function (setting, node) {
                    var aObj = $$(node, consts.id.A, setting),
                        url = view.makeNodeUrl(setting, node);
                    if (url == null || url.length == 0) {
                        aObj.removeAttr("href");
                    } else {
                        aObj.attr("href", url);
                    }
                },
                switchNode: function (setting, node) {
                    if (node.open || !tools.canAsync(setting, node)) {
                        view.expandCollapseNode(setting, node, !node.open);
                    } else if (setting.async.enable) {
                        if (!view.asyncNode(setting, node)) {
                            view.expandCollapseNode(setting, node, !node.open);
                            return;
                        }
                    } else if (node) {
                        view.expandCollapseNode(setting, node, !node.open);
                    }
                }
            };
        // zTree defind
        $.fn.zTree = {
            consts: _consts,
            _z: {
                tools: tools,
                view: view,
                event: event,
                data: data
            },
            getZTreeObj: function (treeId) {
                var o = data.getZTreeTools(treeId);
                return o ? o : null;
            },
            destroy: function (treeId) {
                if (!!treeId && treeId.length > 0) {
                    view.destroy(data.getSetting(treeId));
                } else {
                    for (var s in settings) {
                        view.destroy(settings[s]);
                    }
                }
            },
            init: function (obj, zSetting, zNodes) {
                var setting = tools.clone(_setting);
                $.extend(true, setting, zSetting);
                setting.treeId = obj.attr("id");
                setting.treeObj = obj;
                setting.treeObj.empty();
                settings[setting.treeId] = setting;
                //For some older browser,(e.g., ie6)
                if (typeof document.body.style.maxHeight === "undefined") {
                    setting.view.expandSpeed = "";
                }
                data.initRoot(setting);
                var root = data.getRoot(setting);
                zNodes = zNodes ? tools.clone(tools.isArray(zNodes) ? zNodes : [zNodes]) : [];
                if (setting.data.simpleData.enable) {
                    data.nodeChildren(setting, root, data.transformTozTreeFormat(setting, zNodes));
                } else {
                    data.nodeChildren(setting, root, zNodes);
                }

                data.initCache(setting);
                event.unbindTree(setting);
                event.bindTree(setting);
                event.unbindEvent(setting);
                event.bindEvent(setting);

                var zTreeTools = {
                    setting: setting,
                    addNodes: function (parentNode, index, newNodes, isSilent) {
                        if (!parentNode) parentNode = null;
                        var isParent = data.nodeIsParent(setting, parentNode);
                        if (parentNode && !isParent && setting.data.keep.leaf) return null;

                        var i = parseInt(index, 10);
                        if (isNaN(i)) {
                            isSilent = !!newNodes;
                            newNodes = index;
                            index = -1;
                        } else {
                            index = i;
                        }
                        if (!newNodes) return null;


                        var xNewNodes = tools.clone(tools.isArray(newNodes) ? newNodes : [newNodes]);

                        function addCallback() {
                            view.addNodes(setting, parentNode, index, xNewNodes, (isSilent == true));
                        }

                        if (tools.canAsync(setting, parentNode)) {
                            view.asyncNode(setting, parentNode, isSilent, addCallback);
                        } else {
                            addCallback();
                        }
                        return xNewNodes;
                    },
                    cancelSelectedNode: function (node) {
                        view.cancelPreSelectedNode(setting, node);
                    },
                    destroy: function () {
                        view.destroy(setting);
                    },
                    expandAll: function (expandFlag) {
                        expandFlag = !!expandFlag;
                        view.expandCollapseSonNode(setting, null, expandFlag, true);
                        return expandFlag;
                    },
                    expandNode: function (node, expandFlag, sonSign, focus, callbackFlag) {
                        if (!node || !data.nodeIsParent(setting, node)) return null;
                        if (expandFlag !== true && expandFlag !== false) {
                            expandFlag = !node.open;
                        }
                        callbackFlag = !!callbackFlag;

                        if (callbackFlag && expandFlag && (tools.apply(setting.callback.beforeExpand, [setting.treeId, node], true) == false)) {
                            return null;
                        } else if (callbackFlag && !expandFlag && (tools.apply(setting.callback.beforeCollapse, [setting.treeId, node], true) == false)) {
                            return null;
                        }
                        if (expandFlag && node.parentTId) {
                            view.expandCollapseParentNode(setting, node.getParentNode(), expandFlag, false);
                        }
                        if (expandFlag === node.open && !sonSign) {
                            return null;
                        }

                        data.getRoot(setting).expandTriggerFlag = callbackFlag;
                        if (!tools.canAsync(setting, node) && sonSign) {
                            view.expandCollapseSonNode(setting, node, expandFlag, true, showNodeFocus);
                        } else {
                            node.open = !expandFlag;
                            view.switchNode(this.setting, node);
                            showNodeFocus();
                        }
                        return expandFlag;

                        function showNodeFocus() {
                            var a = $$(node, setting).get(0);
                            if (a && focus !== false) {
                                view.scrollIntoView(setting, a);
                            }
                        }
                    },
                    getNodes: function () {
                        return data.getNodes(setting);
                    },
                    getNodeByParam: function (key, value, parentNode) {
                        if (!key) return null;
                        return data.getNodeByParam(setting, parentNode ? data.nodeChildren(setting, parentNode) : data.getNodes(setting), key, value);
                    },
                    getNodeByTId: function (tId) {
                        return data.getNodeCache(setting, tId);
                    },
                    getNodesByParam: function (key, value, parentNode) {
                        if (!key) return null;
                        return data.getNodesByParam(setting, parentNode ? data.nodeChildren(setting, parentNode) : data.getNodes(setting), key, value);
                    },
                    getNodesByParamFuzzy: function (key, value, parentNode) {
                        if (!key) return null;
                        return data.getNodesByParamFuzzy(setting, parentNode ? data.nodeChildren(setting, parentNode) : data.getNodes(setting), key, value);
                    },
                    getNodesByFilter: function (filter, isSingle, parentNode, invokeParam) {
                        isSingle = !!isSingle;
                        if (!filter || (typeof filter != "function")) return (isSingle ? null : []);
                        return data.getNodesByFilter(setting, parentNode ? data.nodeChildren(setting, parentNode) : data.getNodes(setting), filter, isSingle, invokeParam);
                    },
                    getNodeIndex: function (node) {
                        if (!node) return null;
                        var parentNode = (node.parentTId) ? node.getParentNode() : data.getRoot(setting);
                        var children = data.nodeChildren(setting, parentNode);
                        for (var i = 0, l = children.length; i < l; i++) {
                            if (children[i] == node) return i;
                        }
                        return -1;
                    },
                    getSelectedNodes: function () {
                        var r = [], list = data.getRoot(setting).curSelectedList;
                        for (var i = 0, l = list.length; i < l; i++) {
                            r.push(list[i]);
                        }
                        return r;
                    },
                    isSelectedNode: function (node) {
                        return data.isSelectedNode(setting, node);
                    },
                    reAsyncChildNodesPromise: function (parentNode, reloadType, isSilent) {
                        var promise = new Promise(function (resolve, reject) {
                            try {
                                zTreeTools.reAsyncChildNodes(parentNode, reloadType, isSilent, function () {
                                    resolve(parentNode);
                                });
                            } catch (e) {
                                reject(e);
                            }
                        });
                        return promise;
                    },
                    reAsyncChildNodes: function (parentNode, reloadType, isSilent, callback) {
                        if (!this.setting.async.enable) return;
                        var isRoot = !parentNode;
                        if (isRoot) {
                            parentNode = data.getRoot(setting);
                        }
                        if (reloadType == "refresh") {
                            var children = data.nodeChildren(setting, parentNode);
                            for (var i = 0, l = children ? children.length : 0; i < l; i++) {
                                data.removeNodeCache(setting, children[i]);
                            }
                            data.removeSelectedNode(setting);
                            data.nodeChildren(setting, parentNode, []);
                            if (isRoot) {
                                this.setting.treeObj.empty();
                            } else {
                                var ulObj = $$(parentNode, consts.id.UL, setting);
                                ulObj.empty();
                            }
                        }
                        view.asyncNode(this.setting, isRoot ? null : parentNode, !!isSilent, callback);
                    },
                    refresh: function () {
                        this.setting.treeObj.empty();
                        var root = data.getRoot(setting),
                            nodes = data.nodeChildren(setting, root);
                        data.initRoot(setting);
                        data.nodeChildren(setting, root, nodes);
                        data.initCache(setting);
                        view.createNodes(setting, 0, data.nodeChildren(setting, root), null, -1);
                    },
                    removeChildNodes: function (node) {
                        if (!node) return null;
                        var nodes = data.nodeChildren(setting, node);
                        view.removeChildNodes(setting, node);
                        return nodes ? nodes : null;
                    },
                    removeNode: function (node, callbackFlag) {
                        if (!node) return;
                        callbackFlag = !!callbackFlag;
                        if (callbackFlag && tools.apply(setting.callback.beforeRemove, [setting.treeId, node], true) == false) return;
                        view.removeNode(setting, node);
                        if (callbackFlag) {
                            this.setting.treeObj.trigger(consts.event.REMOVE, [setting.treeId, node]);
                        }
                    },
                    selectNode: function (node, addFlag, isSilent) {
                        if (!node) return;
                        if (tools.uCanDo(setting)) {
                            addFlag = setting.view.selectedMulti && addFlag;
                            if (node.parentTId) {
                                view.expandCollapseParentNode(setting, node.getParentNode(), true, false, showNodeFocus);
                            } else if (!isSilent) {
                                try {
                                    $$(node, setting).focus().blur();
                                } catch (e) {
                                }
                            }
                            view.selectNode(setting, node, addFlag);
                        }

                        function showNodeFocus() {
                            if (isSilent) {
                                return;
                            }
                            var a = $$(node, setting).get(0);
                            view.scrollIntoView(setting, a);
                        }
                    },
                    transformTozTreeNodes: function (simpleNodes) {
                        return data.transformTozTreeFormat(setting, simpleNodes);
                    },
                    transformToArray: function (nodes) {
                        return data.transformToArrayFormat(setting, nodes);
                    },
                    updateNode: function (node, checkTypeFlag) {
                        if (!node) return;
                        var nObj = $$(node, setting);
                        if (nObj.get(0) && tools.uCanDo(setting)) {
                            view.setNodeName(setting, node);
                            view.setNodeTarget(setting, node);
                            view.setNodeUrl(setting, node);
                            view.setNodeLineIcos(setting, node);
                            view.setNodeFontCss(setting, node);
                        }
                    }
                };
                root.treeTools = zTreeTools;
                data.setZTreeTools(setting, zTreeTools);
                var children = data.nodeChildren(setting, root);
                if (children && children.length > 0) {
                    view.createNodes(setting, 0, children, null, -1);
                } else if (setting.async.enable && setting.async.url && setting.async.url !== '') {
                    view.asyncNode(setting);
                }
                return zTreeTools;
            }
        };

        var zt = $.fn.zTree,
            $$ = tools.$,
            consts = zt.consts;
    })(jQuery);

    /*
     * JQuery zTree exHideNodes v3.5.37
     * http://treejs.cn/
     *
     * Copyright (c) 2010 Hunter.z
     *
     * Licensed same as jquery - MIT License
     * http://www.opensource.org/licenses/mit-license.php
     *
     * email: hunter.z@263.net
     * Date: 2018-08-21
     */
    (function (j) {
        j.extend(!0, j.fn.zTree._z, {
            view: {
                clearOldFirstNode: function (c, a) { for (var b = a.getNextNode(); b;) { if (b.isFirstNode) { b.isFirstNode = !1; e.setNodeLineIcos(c, b); break } if (b.isLastNode) break; b = b.getNextNode() } }, clearOldLastNode: function (c, a, b) { for (a = a.getPreNode(); a;) { if (a.isLastNode) { a.isLastNode = !1; b && e.setNodeLineIcos(c, a); break } if (a.isFirstNode) break; a = a.getPreNode() } }, makeDOMNodeMainBefore: function (c, a, b) {
                    a = d.isHidden(a, b); c.push("<li ", a ? "style='display:none;' " : "", "id='", b.tId, "' class='",
                        l.className.LEVEL, b.level, "' tabindex='0' hidefocus='true' treenode>")
                }, showNode: function (c, a) { d.isHidden(c, a, !1); d.initShowForExCheck(c, a); k(a, c).show() }, showNodes: function (c, a, b) { if (a && a.length != 0) { var f = {}, g, i; for (g = 0, i = a.length; g < i; g++) { var h = a[g]; if (!f[h.parentTId]) { var u = h.getParentNode(); f[h.parentTId] = u === null ? d.getRoot(c) : h.getParentNode() } e.showNode(c, h, b) } for (var j in f) a = d.nodeChildren(c, f[j]), e.setFirstNodeForShow(c, a), e.setLastNodeForShow(c, a) } }, hideNode: function (c, a) {
                    d.isHidden(c, a,
                        !0); a.isFirstNode = !1; a.isLastNode = !1; d.initHideForExCheck(c, a); e.cancelPreSelectedNode(c, a); k(a, c).hide()
                }, hideNodes: function (c, a, b) { if (a && a.length != 0) { var f = {}, g, i; for (g = 0, i = a.length; g < i; g++) { var h = a[g]; if ((h.isFirstNode || h.isLastNode) && !f[h.parentTId]) { var j = h.getParentNode(); f[h.parentTId] = j === null ? d.getRoot(c) : h.getParentNode() } e.hideNode(c, h, b) } for (var k in f) a = d.nodeChildren(c, f[k]), e.setFirstNodeForHide(c, a), e.setLastNodeForHide(c, a) } }, setFirstNode: function (c, a) {
                    var b = d.nodeChildren(c, a), f =
                        d.isHidden(c, b[0], !1); b.length > 0 && !f ? b[0].isFirstNode = !0 : b.length > 0 && e.setFirstNodeForHide(c, b)
                }, setLastNode: function (c, a) { var b = d.nodeChildren(c, a), f = d.isHidden(c, b[0]); b.length > 0 && !f ? b[b.length - 1].isLastNode = !0 : b.length > 0 && e.setLastNodeForHide(c, b) }, setFirstNodeForHide: function (c, a) { var b, f, g; for (f = 0, g = a.length; f < g; f++) { b = a[f]; if (b.isFirstNode) break; if (!d.isHidden(c, b) && !b.isFirstNode) { b.isFirstNode = !0; e.setNodeLineIcos(c, b); break } else b = null } return b }, setFirstNodeForShow: function (c, a) {
                    var b, f,
                        g, i, h; for (f = 0, g = a.length; f < g; f++) { b = a[f]; var j = d.isHidden(c, b); if (!i && !j && b.isFirstNode) { i = b; break } else if (!i && !j && !b.isFirstNode) b.isFirstNode = !0, i = b, e.setNodeLineIcos(c, b); else if (i && b.isFirstNode) { b.isFirstNode = !1; h = b; e.setNodeLineIcos(c, b); break } } return { "new": i, old: h }
                }, setLastNodeForHide: function (c, a) { var b, f; for (f = a.length - 1; f >= 0; f--) { b = a[f]; if (b.isLastNode) break; if (!d.isHidden(c, b) && !b.isLastNode) { b.isLastNode = !0; e.setNodeLineIcos(c, b); break } else b = null } return b }, setLastNodeForShow: function (c,
                    a) { var b, f, g, i; for (f = a.length - 1; f >= 0; f--) { b = a[f]; var h = d.isHidden(c, b); if (!g && !h && b.isLastNode) { g = b; break } else if (!g && !h && !b.isLastNode) b.isLastNode = !0, g = b, e.setNodeLineIcos(c, b); else if (g && b.isLastNode) { b.isLastNode = !1; i = b; e.setNodeLineIcos(c, b); break } } return { "new": g, old: i } }
            }, data: {
                initHideForExCheck: function (c, a) {
                    if (d.isHidden(c, a) && c.check && c.check.enable) {
                        if (typeof a._nocheck == "undefined") a._nocheck = !!a.nocheck, a.nocheck = !0; a.check_Child_State = -1; e.repairParentChkClassWithSelf && e.repairParentChkClassWithSelf(c,
                            a)
                    }
                }, initShowForExCheck: function (c, a) { if (!d.isHidden(c, a) && c.check && c.check.enable) { if (typeof a._nocheck != "undefined") a.nocheck = a._nocheck, delete a._nocheck; if (e.setChkClass) { var b = k(a, l.id.CHECK, c); e.setChkClass(c, b, a) } e.repairParentChkClassWithSelf && e.repairParentChkClassWithSelf(c, a) } }
            }
        }); var j = j.fn.zTree, m = j._z.tools, l = j.consts, e = j._z.view, d = j._z.data, k = m.$; d.isHidden = function (c, a, b) {
            if (!a) return !1; c = c.data.key.isHidden; typeof b !== "undefined" && (typeof b === "string" && (b = m.eqs(checked, "true")), a[c] =
                !!b); return a[c]
        }; d.exSetting({ data: { key: { isHidden: "isHidden" } } }); d.addInitNode(function (c, a, b) { a = d.isHidden(c, b); d.isHidden(c, b, a); d.initHideForExCheck(c, b) }); d.addBeforeA(function () { }); d.addZTreeTools(function (c, a) { a.showNodes = function (a, b) { e.showNodes(c, a, b) }; a.showNode = function (a, b) { a && e.showNodes(c, [a], b) }; a.hideNodes = function (a, b) { e.hideNodes(c, a, b) }; a.hideNode = function (a, b) { a && e.hideNodes(c, [a], b) }; var b = a.checkNode; if (b) a.checkNode = function (f, e, i, h) { (!f || !d.isHidden(c, f)) && b.apply(a, arguments) } });
        var n = d.initNode; d.initNode = function (c, a, b, f, g, i, h) { var j = (f ? f : d.getRoot(c))[c.data.key.children]; d.tmpHideFirstNode = e.setFirstNodeForHide(c, j); d.tmpHideLastNode = e.setLastNodeForHide(c, j); h && (e.setNodeLineIcos(c, d.tmpHideFirstNode), e.setNodeLineIcos(c, d.tmpHideLastNode)); g = d.tmpHideFirstNode === b; i = d.tmpHideLastNode === b; n && n.apply(d, arguments); h && i && e.clearOldLastNode(c, b, h) }; var o = d.makeChkFlag; if (o) d.makeChkFlag = function (c, a) { (!a || !d.isHidden(c, a)) && o.apply(d, arguments) }; var p = d.getTreeCheckedNodes;
        if (p) d.getTreeCheckedNodes = function (c, a, b, f) { if (a && a.length > 0) { var e = a[0].getParentNode(); if (e && d.isHidden(c, e)) return [] } return p.apply(d, arguments) }; var q = d.getTreeChangeCheckedNodes; if (q) d.getTreeChangeCheckedNodes = function (c, a, b) { if (a && a.length > 0) { var e = a[0].getParentNode(); if (e && d.isHidden(c, e)) return [] } return q.apply(d, arguments) }; var r = e.expandCollapseSonNode; if (r) e.expandCollapseSonNode = function (c, a, b, f, g) { (!a || !d.isHidden(c, a)) && r.apply(e, arguments) }; var s = e.setSonNodeCheckBox; if (s) e.setSonNodeCheckBox =
            function (c, a, b, f) { (!a || !d.isHidden(c, a)) && s.apply(e, arguments) }; var t = e.repairParentChkClassWithSelf; if (t) e.repairParentChkClassWithSelf = function (c, a) { (!a || !d.isHidden(c, a)) && t.apply(e, arguments) }
    })(jQuery);




    TreeSelect.prototype.render = function (options) {
        var elem = options.elem,
            // 请求地址
            url = options.url,
            data = options.data,
            // 请求头
            headers = options.headers,
            // 请求方式
            type = options.type === undefined ? 'GET' : options.type,
            // 节点点击回调
            click = options.click,
            // 渲染成功后的回调函数
            success = options.success,
            // 占位符（提示信息）
            placeholder = options.placeholder === undefined ? '请选择' : options.placeholder,
            // 是否开启搜索
            search = options.search === undefined ? false : options.search,
            // 样式配置项
            style = options.style,
            // parseData = options.parseData,
            // 唯一id
            tmp = new Date().getTime(),
            DATA = {},
            selected = 'layui-form-selected',
            TREE_OBJ = undefined,
            TREE_INPUT_ID = 'treeSelect-input-' + tmp,
            TREE_INPUT_CLASS = 'layui-treeselect',
            TREE_SELECT_ID = 'layui-treeSelect-' + tmp,
            TREE_SELECT_CLASS = 'layui-treeSelect',
            TREE_SELECT_TITLE_ID = 'layui-select-title-' + tmp,
            TREE_SELECT_TITLE_CLASS = 'layui-select-title',
            TREE_SELECT_BODY_ID = 'layui-treeSelect-body-' + tmp,
            TREE_SELECT_BODY_CLASS = 'layui-treeSelect-body',
            TREE_SELECT_SEARCHED_CLASS = 'layui-treeSelect-search-ed';


        var a = {
            init: function () {

                if (data && data.constructor === Array) {
                    a.hideElem().input().toggleSelect().loadCss().preventEvent();
                    $.fn.zTree.init($('#' + TREE_SELECT_BODY_ID), a.setting(), data);
                    TREE_OBJ = $.fn.zTree.getZTreeObj(TREE_SELECT_BODY_ID);
                    if (search) {
                        a.searchParam();
                    }
                    a.configStyle();
                    if (success) {
                        var obj = {
                            treeId: TREE_SELECT_ID,
                            data: data
                        };
                        success(obj);
                    }
                    return;
                }

                var tmpParam = data;
                if (tmpParam) {
                    var AntiforgeryToken = tmpParam["__RequestVerificationToken"];
                    if (AntiforgeryToken != undefined) {
                        headers["__RequestVerificationToken"] = AntiforgeryToken;
                    }
                } else {
                    tmpParam = {};
                }
                if (options.useRsa) {
                    tmpParam = $.encrypt(tmpParam);
                }

                $.ajax({
                    type: type,
                    url: url,
                    contentType: "application/json;charset=UTF-8",
                    data: JSON.stringify(tmpParam),
                    dataType: "JSON",
                    headers: headers,
                    success: function (d) {
                        DATA = d;
                        if (options.useRsa) {
                            if (DATA != undefined && DATA.data != undefined && typeof DATA.data === 'object' && DATA.data.constructor === Array) {
                                DATA = DATA.data;
                            }
                            if (DATA.ciphertext != undefined && DATA.ciphertext.length > 2) {
                                DATA = JSON.parse($.decrypt(DATA.ciphertext,url));
                            }
                        }
                        a.hideElem().input().toggleSelect().loadCss().preventEvent();
                        $.fn.zTree.init($('#' + TREE_SELECT_BODY_ID), a.setting(), DATA);
                        TREE_OBJ = $.fn.zTree.getZTreeObj(TREE_SELECT_BODY_ID);
                        if (search) {
                            a.searchParam();
                        }
                        a.configStyle();
                        if (success) {
                            var obj = {
                                treeId: TREE_SELECT_ID,
                                data: DATA
                            };
                            success(obj);
                        }
                    }
                });
                return a;
            },
            // 检查input是否有默认值
            checkDefaultValue: function () {

            },
            setting: function () {
                var setting = {
                    data: {
                        simpleData: { enable: true }
                    },
                    callback: {
                        onClick: a.onClick,
                        onExpand: a.onExpand,
                        onCollapse: a.onCollapse,
                        beforeExpand: a.ztreeCallBack.beforeExpand
                    }
                };
                return setting;
            },
            ztreeCallBack: {
                beforeExpand: function () {
                    a.configStyle();
                },
            },
            onCollapse: function () {
                a.focusInput();
            },
            onExpand: function () {
                a.configStyle();
                a.focusInput();
            },
            focusInput: function () {
                $('#' + TREE_INPUT_ID).focus();
            },
            onClick: function (event, treeId, treeNode) {
                var name = treeNode.name,
                    id = treeNode.id,
                    $input = $('#' + TREE_SELECT_TITLE_ID + ' input');
                $input.val(name);
                $(elem).attr('value', id).val(id);
                $('#' + TREE_SELECT_ID).removeClass(selected);

                if (click) {
                    var obj = {
                        data: DATA,
                        current: treeNode,
                        treeId: TREE_SELECT_ID
                    };
                    click(obj);
                }
                return a;
            },
            hideElem: function () {
                $(elem).hide();
                return a;
            },
            input: function () {
                var readonly = '';
                if (!search) {
                    readonly = 'readonly';
                }
                var selectHtml = '<div class="' + TREE_SELECT_CLASS + ' layui-unselect layui-form-select" id="' + TREE_SELECT_ID + '">' +
                    '<div class="' + TREE_SELECT_TITLE_CLASS + '" id="' + TREE_SELECT_TITLE_ID + '">' +
                    ' <input type="text" id="' + TREE_INPUT_ID + '" placeholder="' + placeholder + '" value="" ' + readonly + ' class="layui-input layui-unselect">' +
                    '<i class="layui-edge"></i>' +
                    '</div>' +
                    '<div class="layui-anim layui-anim-upbit" style="">' +
                    '<div class="' + TREE_SELECT_BODY_CLASS + ' ztree" id="' + TREE_SELECT_BODY_ID + '"></div>' +
                    '</div>' +
                    '</div>';
                $(elem).parent().append(selectHtml);
                return a;
            },
            /**
             * 展开/折叠下拉框
             */
            toggleSelect: function () {
                var item = '#' + TREE_SELECT_TITLE_ID;
                a.event('click', item, function (e) {
                    var $select = $('#' + TREE_SELECT_ID);
                    if ($select.hasClass(selected)) {
                        $select.removeClass(selected);
                        $('#' + TREE_INPUT_ID).blur();
                    } else {
                        // 隐藏其他picker
                        $('.layui-form-select').removeClass(selected);
                        // 显示当前picker
                        $select.addClass(selected);
                    }
                    e.stopPropagation();
                });
                $(document).click(function () {
                    var $select = $('#' + TREE_SELECT_ID);
                    if ($select.hasClass(selected)) {
                        $select.removeClass(selected);
                        $('#' + TREE_INPUT_ID).blur();
                    }
                });
                return a;
            },
            // 模糊查询
            searchParam: function () {
                if (!search) {
                    return;
                }

                var item = '#' + TREE_INPUT_ID;
                a.fuzzySearch(item, null, true);
            },
            fuzzySearch: function (searchField, isHighLight, isExpand) {
                var zTreeObj = TREE_OBJ;//get the ztree object by ztree id
                if (!zTreeObj) {
                    alert("fail to get ztree object");
                }
                var nameKey = zTreeObj.setting.data.key.name; //get the key of the node name
                isHighLight = isHighLight === false ? false : true;//default true, only use false to disable highlight
                isExpand = isExpand ? true : false; // not to expand in default
                zTreeObj.setting.view.nameIsHTML = isHighLight; //allow use html in node name for highlight use

                var metaChar = '[\\[\\]\\\\\^\\$\\.\\|\\?\\*\\+\\(\\)]'; //js meta characters
                var rexMeta = new RegExp(metaChar, 'gi');//regular expression to match meta characters

                // keywords filter function 
                function ztreeFilter(zTreeObj, _keywords, callBackFunc) {
                    if (!_keywords) {
                        _keywords = ''; //default blank for _keywords 
                    }

                    // function to find the matching node
                    function filterFunc(node) {
                        if (node && node.oldname && node.oldname.length > 0) {
                            node[nameKey] = node.oldname; //recover oldname of the node if exist
                        }
                        zTreeObj.updateNode(node); //update node to for modifications take effect
                        if (_keywords.length == 0) {
                            //return true to show all nodes if the keyword is blank
                            zTreeObj.showNode(node);
                            zTreeObj.expandNode(node, isExpand);
                            return true;
                        }
                        //transform node name and keywords to lowercase
                        if (node[nameKey] && node[nameKey].toLowerCase().indexOf(_keywords.toLowerCase()) != -1) {
                            zTreeObj.showNode(node);//show node with matching keywords
                            return true; //return true and show this node
                        }

                        zTreeObj.hideNode(node); // hide node that not matched
                        return false; //return false for node not matched
                    }

                    var nodesShow = zTreeObj.getNodesByFilter(filterFunc); //get all nodes that would be shown
                    processShowNodes(nodesShow, _keywords);//nodes should be reprocessed to show correctly
                }

                /**
                 * reprocess of nodes before showing
                 */
                function processShowNodes(nodesShow, _keywords) {
                    if (nodesShow && nodesShow.length > 0) {
                        //process the ancient nodes if _keywords is not blank
                        if (_keywords.length > 0) {
                            $.each(nodesShow, function (n, obj) {
                                var pathOfOne = obj.getPath();//get all the ancient nodes including current node
                                if (pathOfOne && pathOfOne.length > 0) {
                                    //i < pathOfOne.length-1 process every node in path except self
                                    for (var i = 0; i < pathOfOne.length - 1; i++) {
                                        zTreeObj.showNode(pathOfOne[i]); //show node 
                                        zTreeObj.expandNode(pathOfOne[i], true); //expand node
                                    }
                                }
                            });
                        } else { //show all nodes when _keywords is blank and expand the root nodes
                            var rootNodes = zTreeObj.getNodesByParam('level', '0');//get all root nodes
                            $.each(rootNodes, function (n, obj) {
                                zTreeObj.expandNode(obj, true); //expand all root nodes
                            });
                        }
                    }
                }

                //listen to change in input element
                $(searchField).bind('input propertychange', function () {
                    var _keywords = $(this).val();
                    searchNodeLazy(_keywords); //call lazy load
                });

                var timeoutId = null;
                // excute lazy load once after input change, the last pending task will be cancled  
                function searchNodeLazy(_keywords) {
                    if (timeoutId) {
                        //clear pending task
                        clearTimeout(timeoutId);
                    }
                    timeoutId = setTimeout(function () {
                        ztreeFilter(zTreeObj, _keywords); //lazy load ztreeFilter function 
                        $(searchField).focus();//focus input field again after filtering
                    }, 500);
                }
            },
            checkNodes: function (nodes) {
                for (var i = 0; i < nodes.length; i++) {
                    var o = nodes[i],
                        pid = o.parentTId,
                        tid = o.tId;
                    if (pid !== null) {
                        // 获取父节点
                        $('#' + pid).addClass(TREE_SELECT_SEARCHED_CLASS);
                        var pNode = TREE_OBJ.getNodesByParam("tId", pid, null);
                        TREE_OBJ.expandNode(pNode[0], true, false, true);
                    }
                    $('#' + tid).addClass(TREE_SELECT_SEARCHED_CLASS);
                }
            },
            // 阻止Layui的一些默认事件
            preventEvent: function () {
                var item = '#' + TREE_SELECT_ID + ' .layui-anim';
                a.event('click', item, function (e) {
                    e.stopPropagation();
                });
                return a;
            },
            loadCss: function () {
                var ztree = '.ztree *{padding:0;margin:0;font-size:12px;font-family:Verdana,Arial,Helvetica,AppleGothic,sans-serif}.ztree{margin:0;padding:5px;color:#333}.ztree li{padding:0;margin:0;list-style:none;line-height:14px;text-align:left;white-space:nowrap;outline:0}.ztree li ul{margin:0;padding:0 0 0 18px}.ztree li ul.line{background:url(./img/line_conn.gif) 0 0 repeat-y;}.ztree li a{padding:1px 3px 0 0;margin:0;cursor:pointer;height:17px;color:#333;background-color:transparent;text-decoration:none;vertical-align:top;display:inline-block}.ztree li a:hover{text-decoration:underline}.ztree li a.curSelectedNode{padding-top:0px;background-color:#FFE6B0;color:black;height:16px;border:1px #FFB951 solid;opacity:0.8;}.ztree li a.curSelectedNode_Edit{padding-top:0px;background-color:#FFE6B0;color:black;height:16px;border:1px #FFB951 solid;opacity:0.8;}.ztree li a.tmpTargetNode_inner{padding-top:0px;background-color:#316AC5;color:white;height:16px;border:1px #316AC5 solid;opacity:0.8;filter:alpha(opacity=80)}.ztree li a.tmpTargetNode_prev{}.ztree li a.tmpTargetNode_next{}.ztree li a input.rename{height:14px;width:80px;padding:0;margin:0;font-size:12px;border:1px #7EC4CC solid;*border:0px}.ztree li span{line-height:16px;margin-right:2px}.ztree li span.button{line-height:0;margin:0;width:16px;height:16px;display:inline-block;vertical-align:middle;border:0 none;cursor:pointer;outline:none;background-color:transparent;background-repeat:no-repeat;background-attachment:scroll;background-image:url("./img/zTreeStandard.png");*background-image:url("./img/zTreeStandard.gif")}.ztree li span.button.chk{width:13px;height:13px;margin:0 3px 0 0;cursor:auto}.ztree li span.button.chk.checkbox_false_full{background-position:0 0}.ztree li span.button.chk.checkbox_false_full_focus{background-position:0 -14px}.ztree li span.button.chk.checkbox_false_part{background-position:0 -28px}.ztree li span.button.chk.checkbox_false_part_focus{background-position:0 -42px}.ztree li span.button.chk.checkbox_false_disable{background-position:0 -56px}.ztree li span.button.chk.checkbox_true_full{background-position:-14px 0}.ztree li span.button.chk.checkbox_true_full_focus{background-position:-14px -14px}.ztree li span.button.chk.checkbox_true_part{background-position:-14px -28px}.ztree li span.button.chk.checkbox_true_part_focus{background-position:-14px -42px}.ztree li span.button.chk.checkbox_true_disable{background-position:-14px -56px}.ztree li span.button.chk.radio_false_full{background-position:-28px 0}.ztree li span.button.chk.radio_false_full_focus{background-position:-28px -14px}.ztree li span.button.chk.radio_false_part{background-position:-28px -28px}.ztree li span.button.chk.radio_false_part_focus{background-position:-28px -42px}.ztree li span.button.chk.radio_false_disable{background-position:-28px -56px}.ztree li span.button.chk.radio_true_full{background-position:-42px 0}.ztree li span.button.chk.radio_true_full_focus{background-position:-42px -14px}.ztree li span.button.chk.radio_true_part{background-position:-42px -28px}.ztree li span.button.chk.radio_true_part_focus{background-position:-42px -42px}.ztree li span.button.chk.radio_true_disable{background-position:-42px -56px}.ztree li span.button.switch{width:18px;height:18px}.ztree li span.button.root_open{background-position:-92px -54px}.ztree li span.button.root_close{background-position:-74px -54px}.ztree li span.button.roots_open{background-position:-92px 0}.ztree li span.button.roots_close{background-position:-74px 0}.ztree li span.button.center_open{background-position:-92px -18px}.ztree li span.button.center_close{background-position:-74px -18px}.ztree li span.button.bottom_open{background-position:-92px -36px}.ztree li span.button.bottom_close{background-position:-74px -36px}.ztree li span.button.noline_open{background-position:-92px -72px}.ztree li span.button.noline_close{background-position:-74px -72px}.ztree li span.button.root_docu{background:none;}.ztree li span.button.roots_docu{background-position:-56px 0}.ztree li span.button.center_docu{background-position:-56px -18px}.ztree li span.button.bottom_docu{background-position:-56px -36px}.ztree li span.button.noline_docu{background:none;}.ztree li span.button.ico_open{margin-right:2px;background-position:-110px -16px;vertical-align:top;*vertical-align:middle}.ztree li span.button.ico_close{margin-right:2px;background-position:-110px 0;vertical-align:top;*vertical-align:middle}.ztree li span.button.ico_docu{margin-right:2px;background-position:-110px -32px;vertical-align:top;*vertical-align:middle}.ztree li span.button.edit{margin-right:2px;background-position:-110px -48px;vertical-align:top;*vertical-align:middle}.ztree li span.button.remove{margin-right:2px;background-position:-110px -64px;vertical-align:top;*vertical-align:middle}.ztree li span.button.ico_loading{margin-right:2px;background:url(./img/loading.gif) no-repeat scroll 0 0 transparent;vertical-align:top;*vertical-align:middle}ul.tmpTargetzTree{background-color:#FFE6B0;opacity:0.8;filter:alpha(opacity=80)}span.tmpzTreeMove_arrow{width:16px;height:16px;display:inline-block;padding:0;margin:2px 0 0 1px;border:0 none;position:absolute;background-color:transparent;background-repeat:no-repeat;background-attachment:scroll;background-position:-110px -80px;background-image:url("./img/zTreeStandard.png");*background-image:url("./img/zTreeStandard.gif")}ul.ztree.zTreeDragUL{margin:0;padding:0;position:absolute;width:auto;height:auto;overflow:hidden;background-color:#cfcfcf;border:1px #00B83F dotted;opacity:0.8;filter:alpha(opacity=80)}.zTreeMask{z-index:10000;background-color:#cfcfcf;opacity:0.0;filter:alpha(opacity=0);position:absolute}',
                    ztree_ex = '.layui-treeSelect .ztree li span.button{font-family:layui-icon!important;font-size:16px;font-style:normal;-webkit-font-smoothing:antialiased;-moz-osx-font-smoothing:grayscale;background:none;line-height:inherit;}.layui-treeSelect .ztree li span.button.ico_open{display:none;}.layui-treeSelect .ztree li span.button.ico_close{display:none;}.layui-treeSelect .ztree li span.button.ico_docu:before{content:"\\e621";}.layui-treeSelect .ztree li span.button.bottom_close:before,.layui-treeSelect .ztree li span.button.center_close:before,.layui-treeSelect .ztree li span.button.roots_close:before,.layui-treeSelect .ztree li span.button.root_close:before{content:"\\e623";}.layui-treeSelect .ztree li span.button.bottom_open:before,.layui-treeSelect .ztree li span.button.roots_open:before,.layui-treeSelect .ztree li span.button.center_open:before,.layui-treeSelect .ztree li span.button.root_open:before{content:"\\e625";}.layui-treeSelect .ztree li a:hover{text-decoration:none;}.layui-treeSelect .ztree *{font-size:14px;}.layui-treeSelect .ztree li{line-height:inherit;padding:2px 0;}.layui-treeSelect .ztree li span.button.switch{position:relative;top:-1px;}.layui-treeSelect .ztree li a,.ztree li span{line-height:18px;height:inherit;}.layui-treeSelect .ztree li a.curSelectedNode{color:#5FB878;background:none;border:none;height:inherit;padding-top:1px;}.layui-treeSelect .layui-anim::-webkit-scrollbar{width:6px;height:6px;background-color:#F5F5F5;}.layui-treeSelect .layui-anim::-webkit-scrollbar-track{box-shadow:inset 0 0 6px rgba(107,98,98,0.3);border-radius:10px;background-color:#F5F5F5;}.layui-treeSelect .layui-anim::-webkit-scrollbar-thumb{border-radius:10px;box-shadow:inset 0 0 6px rgba(107,98,98,0.3);background-color:#555;}.layui-treeSelect.layui-form-select .layui-anim{display:none;position:absolute;left:0;top:42px;padding:5px 0;z-index:9999;min-width:100%;border:1px solid #d2d2d2;max-height:300px;overflow-y:auto;background-color:#fff;border-radius:2px;box-shadow:0 2px 4px rgba(0,0,0,.12);box-sizing:border-box;}.layui-treeSelect.layui-form-selected .layui-anim{display:block;}.layui-treeSelect .ztree li ul.line{background:none;position:relative;}.layui-treeSelect .ztree li ul.line:before{content:"";height:100%;border-left:1px dotted #ece;position:absolute;left:8px;}.layui-treeSelect .ztree li .center_docu:before,.ztree li .bottom_docu::before{content:"";height:100%;border-left:1px dotted #ece;position:absolute;left:8px;}.layui-treeSelect .ztree li .center_docu::after,.ztree li .bottom_docu::after{content:"";position:absolute;left:8px;top:8px;width:8px;border-top:1px dotted #ece;}.layui-treeSelect .ztree li span.button.ico_open{display:inline-block;position:relative;top:1px;}.layui-treeSelect .ztree li span.button.ico_close{display:inline-block;position:relative;top:1px;}.layui-treeSelect .ztree li span.button.ico_open:before{content:"\\e643";}.layui-treeSelect .ztree li span.button.ico_close:before{content:"\\e63f";}';
                $head = $('head'),
                    ztreeStyle = $head.find('style[ztree]');

                if (ztreeStyle.length === 0) {
                    $head.append($('<style ztree>').append(ztree).append(ztree_ex))
                }
                return a;
            },
            configStyle: function () {
                if (style == undefined || style.line == undefined || !style.line.enable) {
                    $('#' + TREE_SELECT_ID).find('li .center_docu,li .bottom_docu').hide();
                    //.layui-treeSelect .ztree li .center_docu:before, .ztree li .bottom_docu::before
                }

                if (style == undefined || style.folder == undefined || !style.folder.enable) {
                    $('#' + TREE_SELECT_ID).find('li span.button.ico_open').hide();
                    $('#' + TREE_SELECT_ID).find('li span.button.ico_close').hide();
                }
            },
            event: function (evt, el, fn) {
                $('body').on(evt, el, fn);
            }
        };
        a.init();
        return new TreeSelect();
    };

    /**
     * 重新加载trerSelect
     * @param filter
     */
    TreeSelect.prototype.refresh = function (filter) {
        var treeObj = obj.treeObj(filter);
        treeObj.reAsyncChildNodes(null, "refresh");
    };

    /**
     * 选中节点，因为tree是异步加载，所以必须在success回调中调用checkNode函数，否则无法获取生成的DOM元素
     * @param filter lay-filter属性
     * @param id 选中的id
     */
    TreeSelect.prototype.checkNode = function (filter, id) {
        var o = obj.filter(filter),
            treeInput = o.find('.layui-select-title input'),
            treeObj = obj.treeObj(filter),
            node = treeObj.getNodeByParam("id", id, null),
            name = node.name;
        treeInput.val(name);
        o.find('a[treenode_a]').removeClass('curSelectedNode');
        obj.get(filter).val(id).attr('value', id);
        treeObj.selectNode(node);
    };

    /**
     * 撤销选中的节点
     * @param filter lay-filter属性
     * @param fn 回调函数
     */
    TreeSelect.prototype.revokeNode = function (filter, fn) {
        var o = obj.filter(filter);
        o.find('a[treenode_a]').removeClass('curSelectedNode');
        o.find('.layui-select-title input.layui-input').val('');
        obj.get(filter).attr('value', '').val('');
        obj.treeObj(filter).expandAll(false);
        if (fn) {
            fn({
                treeId: o.attr('id')
            });
        }
    }

    /**
     * 销毁组件
     */
    TreeSelect.prototype.destroy = function (filter) {
        var o = obj.filter(filter);
        o.remove();
        obj.get(filter).show();
    }

    /**
     * 获取zTree对象，可调用所有zTree函数
     * @param filter
     */
    TreeSelect.prototype.zTree = function (filter) {
        return obj.treeObj(filter);
    };

    var obj = {
        get: function (filter) {
            if (!filter) {
                layui.hint().error('filter 不能为空');
            }
            return $('*[lay-filter=' + filter + ']');
        },
        filter: function (filter) {
            var tf = obj.get(filter),
                o = tf.next();
            return o;
        },
        treeObj: function (filter) {
            var o = obj.filter(filter),
                treeId = o.find('.layui-treeSelect-body').attr('id'),
                tree = $.fn.zTree.getZTreeObj(treeId);
            return tree;
        }
    };

    //输出接口
    var mod = new TreeSelect();
    exports(_MOD, mod);
});    