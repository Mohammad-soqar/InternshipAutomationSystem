let fillAnimationDelay = 100; // Adjust the delay time as needed

$(document).ready(startProgressAnimation);

function startProgressAnimation() {
    const triggerCircle = $('.progress-circle.trigger-this');
    const triggerLine = $('.progress-line.trigger-this2');

    // Reset animation classes
    $('.progress-circle').removeClass('progress-animation');
    $('.progress-line').removeClass('progress-animation2');

    // Delay the fill animation
    setTimeout(function () {
        triggerCircle.addClass('progress-animation');
        triggerLine.addClass('progress-animation2');
    }, fillAnimationDelay);
}