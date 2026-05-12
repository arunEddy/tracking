const API = AppConfig.apiBaseUrl + "/api/FirstMileRTO";

let modal;

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('rtoModal'));

    loadData();

    $("#btnAdd").click(function () {

        resetForm();

        modal.show();

    });

    $("#btnSave").click(function () {

        saveData();

    });

});

function loadData() {

    $.ajax({

        url: API,
        type: "GET",

        success: function (res) {

            let data = res?.data || res;

            let html = "";

            $.each(data, function (i, item) {

                html += `

                    <tr>

                        <td>${i + 1}</td>

                        
                        <td>${item.awbNumber || ''}</td>

                        <td>${formatDate(item.rtoDate)}</td>

                        <td>${item.rtoReason || ''}</td>

                        <td>

                            <button class="btn btn-primary btn-sm"
                                    onclick="edit(${item.rtoid})">

                                <i class="bi bi-pencil"></i>

                            </button>

                            <button class="btn btn-danger btn-sm"
                                    onclick="deleteRec(${item.rtoid})">

                                <i class="bi bi-trash"></i>

                            </button>

                        </td>

                    </tr>

                `;

            });

            $("#tblData").html(html);

        },

        error: function () {

            Swal.fire(
                'Error',
                'Failed to load data',
                'error'
            );

        }

    });

}

function saveData() {

    if ($("#AWBNumber").val() == "") {

        Swal.fire(
            'Validation',
            'AWB Number Required',
            'warning'
        );

        return;
    }

    if ($("#RTODate").val() == "") {

        Swal.fire(
            'Validation',
            'RTO Date Required',
            'warning'
        );

        return;
    }

    if ($("#RTOReason").val() == "") {

        Swal.fire(
            'Validation',
            'RTO Reason Required',
            'warning'
        );

        return;
    }

    let id = $("#rtoid").val();

    let obj = {

        rtoid: id || 0,

       

        awbNumber: $("#AWBNumber").val(),

        rtoDate: $("#RTODate").val(),

        rtoReason: $("#RTOReason").val(),

        createdby: "ADMIN",

        isActive: "Y"
    };

    $.ajax({

        url: id ? `${API}/${id}` : API,

        type: id ? "PUT" : "POST",

        contentType: "application/json",

        data: JSON.stringify(obj),

        beforeSend: function () {

            $("#btnSave")
                .prop('disabled', true)
                .html('<span class="spinner-border spinner-border-sm"></span> Saving...');

        },

        success: function () {

            Swal.fire({
                icon: 'success',
                title: id ? 'Updated Successfully' : 'Saved Successfully',
                timer: 2000,
                showConfirmButton: false
            });

            modal.hide();

            loadData();

            resetForm();

        },

        error: function (xhr) {

            Swal.fire(
                'Error',
                xhr.responseText || 'Operation failed',
                'error'
            );

        },

        complete: function () {

            $("#btnSave")
                .prop('disabled', false)
                .html('<i class="bi bi-save"></i> Save');

        }

    });

}

function edit(id) {

    $.ajax({

        url: `${API}/${id}`,
        type: "GET",

        success: function (res) {

            let d = res?.data || res;

            $("#rtoid").val(d.rtoid);

            

            $("#AWBNumber").val(d.awbNumber);

            $("#RTODate").val(
                d.rtoDate
                    ? new Date(d.rtoDate).toISOString().split('T')[0]
                    : ''
            );

            $("#RTOReason").val(d.rtoReason);

            modal.show();

        }

    });

}

function deleteRec(id) {

    Swal.fire({

        title: 'Delete?',
        text: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true

    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({

                url: `${API}/${id}`,
                type: "DELETE",

                success: function () {

                    Swal.fire(
                        'Deleted',
                        'Record Deleted Successfully',
                        'success'
                    );

                    loadData();

                },

                error: function () {

                    Swal.fire(
                        'Error',
                        'Delete failed',
                        'error'
                    );

                }

            });

        }

    });

}

function resetForm() {

    $("#rtoid").val('');


    $("#AWBNumber").val('');

    $("#RTODate").val('');

    $("#RTOReason").val('');

}

function formatDate(date) {

    if (!date)
        return '';

    return new Date(date).toLocaleDateString();

}