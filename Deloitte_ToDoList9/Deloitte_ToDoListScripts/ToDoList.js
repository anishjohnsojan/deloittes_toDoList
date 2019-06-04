$(document).ready(function () {
    $.ajax(
        {
            url: '/Deloittes_ToDoList/Get_ToDo_List',
            success: function (result) {
                $("#deloitte_toDoList").html(result);
            }
        });

   
});