$(document).ready(function () {
    $(document).on('click', ".isChecked",
        function () {
            var checkbox = $(this);
            var ID = checkbox.attr('id');
            var result = checkbox.prop('checked');
       
 
    $.ajax(
        {
            url: '/Deloittes_ToDoList/Ajax_EditToDoTask',
            data: { id: ID, val: result },
            type: 'POST',
            success: function (result) {
                $('#deloitte_toDoList').html(result);
           }
        });
})
});