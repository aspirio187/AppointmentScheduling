﻿$(document).ready(function () {
    InitializeCalendar();
});

/* Code utilisé dans le tutorial partie '6. Show Calendar' cependant, ça ne fonctionne pas, il faut suivre la fonction suivante */
//function InitializeCalendar() {
//    try {
//        $('#calendar').fullCalendar({
//            timezone: false,
//            header: {
//                left: 'prev,next,today',
//                center: 'title',
//                right: 'month,agendaWeek,agendaDay'
//            },
//            selectable: true,
//            editable: false
//        });
//    }
//    catch (e) {
//        alert(e);
//    }
//}

function InitializeCalendar() {
    try {
        let calendarE1 = document.getElementById('calendar');
        if (calendarE1 !== null) {
            let calendar = new FullCalendar.Calendar(calendarE1, {
                timezone: false,
                header: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                selectable: true,
                editable: false
            });

            calendar.render();
        }
    } catch (e) {
        alert(e);
    }
}