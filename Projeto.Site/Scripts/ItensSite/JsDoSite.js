function eventos(){
    $(".loading").fadeOut(2000, function () {
        $(".container").fadeIn(1000);
    });  
}

window.onload = function(){
    eventos();
}
