$(function () {
    $.ajax({
        url: '/Forms/EmployeesData',
        type: 'GET',
        success: function (data) {
            $.each(data, function (i, item) {
                let formattedDate = moment(item.birth).format('LL');
                let row = $('<tr>').append(
                    $('<td>').text(item.id),
                    $('<td>').text(item.name),
                    $('<td>').text(item.surname),
                    $('<td>').text(formattedDate),
                    $('<td>').text(item.job),
                    $('<td>').text(item.workplaceName)
                );
                $('#employeeTable tbody').append(row);
            });
        }
    });
    $.ajax({
        url: '/Forms/EmployeesWorkplaces',
        type: 'GET',
        success: function (data) {
            $.each(data, function (i, item) {
                let formattedPhone = item.phone.replace(/\D/g, '').replace(/(\d{3})(\d{3})(\d{3})/, '$1-$2-$3');
                let row = $('<tr>').append(
                    $('<td>').text(item.id),
                    $('<td>').text(item.workplaceName),
                    $('<td>').text(item.city),
                    $('<td>').text(item.street),
                    $('<td>').text(item.psc),
                    $('<td>').text(item.email),
                    $('<td>').text(formattedPhone),
                    $('<td>').text(item.employeeCount)
                );
                $('#workplaceTable tbody').append(row);
            });
        }
    });
});