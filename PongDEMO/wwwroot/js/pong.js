
//Routing
$(document).ready(function () {
    $("route").each(function () {
        $(this).css("display", "none");
        var def = $(this).attr('levelDefault');
        if (typeof def !== typeof undefined && def !== false) {
            $(this).css("display", "");
        }
    });
    router();
    $("a").click(function () {
        var isNested = $(this).attr("nested");
        if (typeof isNested !== typeof undefined) {
            $("route").css("display", "none");
        }
    });
});
$(window).bind('hashchange', function () {
    router();
});
function router() {
    $("body").css("display", "");
    var url = window.document.URL.toString();
    var res = url.split("?");
    var route = res[1];
    var level = $("[path=" + route + "]").attr('routeLevel');
    $("[routeLevel='" + level + "'").css("display", "none");
 
    $("[path=" + route + "]").css("display", "");
    $("[path=" + route + "]").parents().css("display", "");
    $("[linkLevel = '" + level + "'").removeClass("active")
    $("[href='#?" + route + "']").addClass("active");
};







//dispatch core get (dispatch only CoreMethod)
function dispatchCoreGet(coreMethod, parentEl, itemKey) {
    $.ajax({
        async: false,
        type: 'GET',
        url: coreMethod,
        data: { itemKey: itemKey },
        success: function (data) {
            $(parentEl).append("<component props itemKey='" + itemKey + "' method='" + coreMethod + "'>" + data + "</component>");
        }
    });
}

//updates selected components (Core Method only)
function updateCoreGet(coreMethod, parentEl, itemKey) {
    $.ajax({
        async: false,
        type: 'GET',
        url: coreMethod,
        data: { itemKey: itemKey },
        success: function (data) {
            $(parentEl + ' >[itemKey="' + itemKey + '"]').html(data);

        }
    });

}



// Split Methods Dispatch
function dispatchGet(higherOrderMethod, coreMethod, parentEl, higherOrderMethodPayload) {
    var parentElVal = "";
    if (parentEl[0] == "#" || parentEl[0] == ".") {
        parentElVal = parentEl.substring(1);
    }
    if (parentEl[0] == "[") {
        var arrTmp = parentEl.split("=");
        var tmpSubstring = arrTmp[1];


            parentElVal = tmpSubstring.slice(1, -2);
  

    
    }

    var inpState = $("#" + parentElVal + "State").val();
    if (inpState == null) {
        $(parentEl).append("<input type='hidden' id='" + parentElVal + "State'    />");
        var state = [];
    }
    else {
        var stateString = $("#" + parentElVal + "State").val();
        var state = stateString.split(',').map(function (el) { return +el; });
    }

    $.ajax({
        async: false,
        type: 'GET',
        url: higherOrderMethod,
        data: higherOrderMethodPayload,
        success: function (data) {
            $("#" + parentElVal + "State").val(data);

            
            
            var diff = [];
            for (var i = 0; i < data.length; i++) {
                var isNew = true;
                for (var j = 0; j < state.length; j++) {
                    if (data[i] == state[j]) {
                        isNew = false;
                    }
                }
                if (isNew) {
                    diff.push(data[i]);
                }

            }
           


            for (var j = 0; j < diff.length; j++) {

                dispatchCoreGet(coreMethod, parentEl, diff[j]);

            }

            var keysToRemove = [];
            for (var i = 0; i < state.length; i++) {
                var shouldRemove = true;
                for (var j = 0; j < data.length; j++) {
                    if (data[j] == state[i]) {
                        shouldRemove = false;
                    }
                }
                if (shouldRemove == true) {
                    keysToRemove.push(state[i]);
                }
            }
            for (var i = 0; i < keysToRemove.length; i++) {
                $(parentEl + ' >[itemKey="' + keysToRemove[i] + '"]').remove();
            }

        }
    });
}
function dispatchGetReload(higherOrderMethod, coreMethod, parentEl, higherOrderMethodPayload) {

    $(parentEl).html("");
    dispatchGet(higherOrderMethod, coreMethod, parentEl, higherOrderMethodPayload);

}


//SET PROP , GET PROP

// changing prop with automatic re-render
function setProp(selector, key, value) {
    var props = $(selector).attr('props');
    var keyIndex = props.search(key);
    var valIndex = keyIndex + key.length + 1;
    var oldVal = "";
    for (var i = valIndex; i < props.length; i++) {
        if (props[i] != "," && props[i] != "'" && props[i] != '"') {
            oldVal += props[i];
        }
        else {
            break;
        }
    }
    var text = props.replace(oldVal, value);
    $(selector).attr('props', text);

 
    var newProps = $(selector).attr('props');
    //re render with changed props
    var tmpProps = newProps;
    var propsArr = tmpProps.split(',');
    var data = {};
    for (var i = 0; i < propsArr.length; i++) {
        var tmpArr = propsArr[i].split(':');
        data[tmpArr[0]] = tmpArr[1];
    }
    var methodUrl = $(selector).attr('method');
    var toAppend;
    $.ajax({
        async: false,
        url: methodUrl,
        type: 'GET',
        data: data,
        success: function (data) {
            toAppend = data;
        }
    });
    $(selector).html(toAppend);


}
//getProp
function getProp(selector, key) {
    var props = $(selector).attr('props');
    var keyIndex = props.search(key);
    var valIndex = keyIndex + key.length + 1;
    var oldVal = "";
    for (var i = valIndex; i < props.length; i++) {
        if (props[i] != "," && props[i] != "'" && props[i] != '"') {
            oldVal += props[i];
        }
        else {
            break;
        }
    }
    return oldVal;
}


//rendering html <component> tags
$(document).ready(function () {

    $("component").each(function () {
        var tmpProps = $(this).attr('props');
        //console.log(tmpProps)
        if (tmpProps != null && tmpProps != "") {
            var propsArr = tmpProps.split(',');
            var data = {};
            for (var i = 0; i < propsArr.length; i++) {
                var tmpArr = propsArr[i].split(':');
                data[tmpArr[0]] = tmpArr[1];
            }
            var methodUrl = $(this).attr('method');
            var toAppend;
            $.ajax({
                async: false,
                url: methodUrl,
                type: 'GET',
                data: data,
                success: function (data) {
                    toAppend = data;
                }
            });
            $(this).html(toAppend);
        }
    });


});

//Render whole partial view dynamically with JS
function dispatchStraight(MvcMethod, parentEl, props) {
   
    
    var data = {};
    var tmpProps = props;

    if (typeof(tmpProps)=='object') {
       
        data = props;
    }
    else {


        var propsArr = tmpProps.split(',');

        var toAppend;
        for (var i = 0; i < propsArr.length; i++) {
            var tmpArr = propsArr[i].split(':');
            data[tmpArr[0]] = tmpArr[1];
        }
    }
    $.ajax({
        async: false,
        url: MvcMethod,
        type: 'GET',
        data: data,
        success: function (data) {
            toAppend = data;
        }
    });
    $(parentEl).append("<component props='" + props + "' method='" + MvcMethod + "'>" + toAppend + "</component>");
}









