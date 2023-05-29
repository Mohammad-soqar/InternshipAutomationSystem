let progressCounter = 0;


$('#startButton').on('click', startProgressAnimation);

function startProgressAnimation() {
    const circle = $('.progress-circle').eq(progressCounter);
    const line = $('.progress-line').eq(progressCounter);

    circle.addClass('progress-animation');
    line.addClass('progress-animation2');

    progressCounter++;

    if (progressCounter >= $('.progress-circle').length) {
        progressCounter = 0;
    }
}


/* dropdown menue for messages Home page student  */

$(document).ready(function () {
    var angle22 = 0;
    $('#dropdown-messages-student').click(function () {
        $('.Messagesslide').slideToggle();
        angle22 += 180;
        angle22 = angle22 % 360;
        $('#down-arrow-home-student').css({
            'transform': 'rotate(' + angle22 + 'deg)',
            'transition': 'transform 0.5s ease-in-out'
        });

        if ((angle22 / 180) % 2 != 0) {
            $('.Messages').css({
                'margin-bottom': '0',
                'transition': 'margin-bottom 0.5s ease-in-out'
            })
        }
        else {
            $('.Messages').css({
                'margin-bottom': 'calc(4.5vh)',
                'transition': 'margin-bottom 0.5s ease-in-out'
            })
        }
    });
});

/* dropdown menue for questions and answers support student page  */
function toggleAnswer(questionClass, answerClass, arrowId, angle) {
    $(questionClass).click(function () {
        $(answerClass).slideToggle();
        angle += 180;
        angle = angle % 360;
        $(arrowId).css({
            'transform': 'rotate(' + angle + 'deg)',
            'transition': 'transform 0.5s ease-in-out'
        });
    });
}
var angle1 = 0;
toggleAnswer('.question1', '.answer1', '#down-arrow1', angle1);

var angle2 = 0;
toggleAnswer('.question2', '.answer2', '#down-arrow2', angle2);

var angle3 = 0;
toggleAnswer('.question3', '.answer3', '#down-arrow3', angle3);

var angle4 = 0;
toggleAnswer('.question4', '.answer4', '#down-arrow4', angle4);

$(document).ready(function () {
    $('#check-all').on('click', function () {
        var rightSectionWidgets = $('.right-section-widgets');
        var widgetLeftSide = $('.widget-left-side');
        var backLeftBtn = $('<button class="back-left-btn" id="back-left">&#x2190; Back</button>');
        var searchInput = $('#searchInput');

        rightSectionWidgets
            .addClass('collapse')
            .css('overflow', 'hidden');

        widgetLeftSide.animate({ width: '90%' }, 500, function () {
            $(this).css('overflow', 'hidden');
        });

        $('#back-buttons').append(backLeftBtn);

        backLeftBtn.on('click', function () {
            searchInput.val("");
            rightSectionWidgets
                .removeClass('collapse')
                .css('overflow', 'hidden');

            widgetLeftSide.animate({ width: '85.5%' }, 500);

            backLeftBtn.remove();
            searchInput.css('display', 'none');
            $('#check-all').css('display', 'block');
            $('tr').css('display', '');

            setTimeout(function () {
                rightSectionWidgets.css('overflow', 'visible');
            }, 200);
        });

        searchInput.css('display', 'block');
        $('#check-all').css('display', 'none');
    });

    $('#Add-User-btn').on('click', function () {
        var leftSection = $('.left-section');
        var rightSectionWidgets = $('.right-section-widgets');
        var widget1 = $('#widget1');
        var backLeftBtn1 = $('<button class="back-left-btn" id="back-left1">&#x2190; Back</button>');

        leftSection
            .addClass('collapse')
            .css({ 'overflow': 'hidden' });

        $('#widget2, #widget3, #widget4').css({ 'display': 'none' });

        rightSectionWidgets.animate({ width: '100%' }, 100, function () {
            $(this).css('overflow', 'hidden');
        });

        $('#back-buttons').append(backLeftBtn1);


        $('.Add-User-Form-invisible').addClass('Add-User-Form');

        $('#back-buttons').append(backLeftBtn1);

        backLeftBtn1.on('click', function () {
            $('#widget2, #widget3, #widget4').css({ 'display': '' });
            $('.Add-User-Form-invisible').removeClass('Add-User-Form');

            leftSection
                .removeClass('collapse')
                .css('overflow', 'hidden');

            rightSectionWidgets.animate({ width: '70%' }, 500);

            backLeftBtn1.remove();


            setTimeout(function () {
                leftSection.css('overflow', 'visible');
            }, 400);
        });



        setTimeout(function () {
            rightSectionWidgets.css('overflow', 'visible');
        }, 200);
    });

    $('#old-HealthI-btn').on('click', function () {
        var leftSection = $('.left-section');
        var rightSectionWidgets = $('.right-section-widgets');
        var widget3 = $('#widget3');
        var backLeftBtn1 = $('<button class="back-left-btn" id="back-left4">&#x2190; Back</button>');

        leftSection
            .addClass('collapse')
            .css({ 'overflow': 'hidden' });

        $('#widget1, #widget3, #widget4').css({ 'display': 'none' });

        rightSectionWidgets.animate({ width: '100%' }, 100, function () {
            $(this).css('overflow', 'hidden');
        });

        $('#back-buttons').append(backLeftBtn1);


        $('#datatable').removeClass('Add-User-Form-invisible');

        $('#back-buttons').append(backLeftBtn1);

        backLeftBtn1.on('click', function () {
            $('#widget1, #widget3, #widget4').css({ 'display': '' });
            $('#datatable').addClass('Add-User-Form-invisible');

            leftSection
                .removeClass('collapse')
                .css('overflow', 'hidden');

            rightSectionWidgets.animate({ width: '70%' }, 500);

            backLeftBtn1.remove();


            setTimeout(function () {
                leftSection.css('overflow', 'visible');
            }, 400);
        });



        setTimeout(function () {
            rightSectionWidgets.css('overflow', 'visible');
        }, 200);
    });

    $('#Add-Announcement-btn').on('click', function () {
        var leftSection = $('.left-section');
        var rightSectionWidgets = $('.right-section-widgets');
        var widget3 = $('#widget3');
        var backLeftBtn1 = $('<button class="back-left-btn" id="back-left3">&#x2190; Back</button>');

        leftSection
            .addClass('collapse')
            .css({ 'overflow': 'hidden' });

        $('#widget2, #widget1, #widget4').css({ 'display': 'none' });

        rightSectionWidgets.animate({ width: '100%' }, 100, function () {
            $(this).css('overflow', 'hidden');
        });

        $('#back-buttons').append(backLeftBtn1);


        $('.Add-User-Form-invisible').addClass('Add-User-Form');

        $('#back-buttons').append(backLeftBtn1);

        backLeftBtn1.on('click', function () {
            $('#widget2, #widget1, #widget4').css({ 'display': '' });
            $('.Add-User-Form-invisible').removeClass('Add-User-Form');

            leftSection
                .removeClass('collapse')
                .css('overflow', 'hidden');

            rightSectionWidgets.animate({ width: '70%' }, 500);

            backLeftBtn1.remove();


            setTimeout(function () {
                leftSection.css('overflow', 'visible');
            }, 400);
        });



        setTimeout(function () {
            rightSectionWidgets.css('overflow', 'visible');
        }, 200);
    });

    $('#Add-Job-btn').on('click', function () {
        var leftSection = $('.left-section');
        var rightSectionWidgets = $('.right-section-widgets');
        var widget3 = $('#widget3');
        var backLeftBtn1 = $('<button class="back-left-btn" id="back-left4">&#x2190; Back</button>');

        leftSection
            .addClass('collapse')
            .css({ 'overflow': 'hidden' });

        $('#widget2, #widget3, #widget1').css({ 'display': 'none' });

        rightSectionWidgets.animate({ width: '100%' }, 100, function () {
            $(this).css('overflow', 'hidden');
        });

        $('#back-buttons').append(backLeftBtn1);


        $('.Add-User-Form-invisible').addClass('Add-User-Form');

        $('#back-buttons').append(backLeftBtn1);

        backLeftBtn1.on('click', function () {
            $('#widget2, #widget3, #widget1').css({ 'display': '' });
            $('.Add-User-Form-invisible').removeClass('Add-User-Form');

            leftSection
                .removeClass('collapse')
                .css('overflow', 'hidden');

            rightSectionWidgets.animate({ width: '70%' }, 500);

            backLeftBtn1.remove();


            setTimeout(function () {
                leftSection.css('overflow', 'visible');
            }, 400);
        });



        setTimeout(function () {
            rightSectionWidgets.css('overflow', 'visible');
        }, 200);
    });
});

$(document).ready(function () {
    $('#search-input').keyup(function () {
        const searchQuery = $(this).val().toLowerCase();

       

        $('#list li').each(function () {
            const text = $(this).text().toLowerCase();
            const match = text.indexOf(searchQuery) !== -1;

            $(this).toggle(match);
        });
    });
});


$(document).click(function (e) {
    var notificationContainer = $(".notificationContiner");
    if (!notificationContainer.is(e.target) && notificationContainer.has(e.target).length === 0) {
        notificationContainer.hide();
    }
});

// Messages box on click


