$(document).ready(function () {
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

        let calendarEl = document.getElementById('calendar');
        let calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            headerToolbar: {
                left: 'prev,next,today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            selectable: true,
            editable: false,
            select: function (event) {
                onShowModal(event, null);
            }
        });
        calendar.render();

    } catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDetails) {
    $("#appointmentInput").modal("show");
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
}