$(document).ready(function (e) {
    var width = $(document).width();



    function launch() {

        $("#rocket").animate({
            bottom: 3500
        }, 6000, function () {

        });
    }

    function countdown() {
        var timeleft = 3;
        var countdownTimer = setInterval(function () {
            if (timeleft <= 0) {
                clearInterval(countdownTimer);
                document.getElementById("countdown").innerHTML = "BLAST OFF!!";
            }
            else {
                document.getElementById("countdown").innerHTML = timeleft;
            }
            timeleft -= 1;

        }, 1000);
    }
    $("#button").click(() => {


        countdown();
        setInterval(function () { launch() }, 4000);


    });
});



