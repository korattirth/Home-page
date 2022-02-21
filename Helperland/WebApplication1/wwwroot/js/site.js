
    const nav=document.querySelector('.nav_bar')
    window.addEventListener('scroll', fixnav)
    function fixnav(){
            if(window.scrollY>nav.offsetHeight+50){
        nav.classList.add('active')
    }
    else{
        nav.classList.remove('active')
    }
        }

$(document).ready(function () {
    $("#for_services").click(function () {
        $("#section_1").show(100);
        $("#section_2").hide();
        $("#section_3").hide();
        $("#section_4").hide();
        $("#for_services").addClass("active")
        $("#Schedule").removeClass("active")
        $("#Details").removeClass("active")
        $("#Payment").removeClass("active")
    })
    $("#Schedule").click(function () {
        $("#section_2").show(200);
        $("#section_1").hide();
        $("#section_3").hide();
        $("#section_4").hide();
        $("#Schedule").addClass("active")
        $("#Details").removeClass("active")
        $("#Payment").removeClass("active")
        $("#for_services").addClass("active")
    })
    $("#Details").click(function () {
        $("#section_3").show(200);
        $("#section_1").hide();
        $("#section_2").hide();
        $("#section_4").hide();
        $("#Details").addClass("active")
        $("#Payment").removeClass("active")
        $("#for_services").addClass("active")
        $("#Schedule").addClass("active")
    })
    $("#Payment").click(function () {
        $("#section_4").show(200);
        $("#section_1").hide();
        $("#section_2").hide();
        $("#section_3").hide();
        $("#Payment").addClass("active")
        $("#for_services").addClass("active")
        $("#Schedule").addClass("active")
        $("#Details").addClass("active")
    })
    $("#for_new_address").click(function () {
        $("#new_address").show(200);
        $("#for_new_address").hide(100)
    })
    $("#cancel").click(function () {
        $("#new_address").hide(200);
        $("#for_new_address").show(100)
    })

    $("#services_1").click(function () {
        $("#services_1").toggleClass("services_border")
    })
    $("#services_2").click(function () {
        $("#services_2").toggleClass("services_border")
    })
    $("#services_3").click(function () {
        $("#services_3").toggleClass("services_border")
    })
    $("#services_4").click(function () {
        $("#services_4").toggleClass("services_border")
    })
    $("#services_5").click(function () {
        $("#services_5").toggleClass("services_border")
    })
})