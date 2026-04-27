const apiUrl = AppConfig.apiBaseUrl + "/api/OfficeTypeMaster";

    function loadData() {
        let search = document.getElementById("searchText").value;

    fetch(apiUrl)
            .then(res => res.json())
            .then(data => {

                if (search) {
        data = data.filter(x => x.officeTypeName.toLowerCase().includes(search.toLowerCase()));
                }

    let rows = "";
    let i = 1;

                data.forEach(x => {
        rows += `<tr>
                        <td>${i++}</td>
                        <td>${x.officeTypeCode}</td>
                        <td>${x.officeTypeName}</td>
                        <td>${x.headOffice ? 'Yes' : 'No'}</td>
                        <td>${x.bookingManifestRequired ? 'Yes' : 'No'}</td>
                        <td>${x.deliveryChargeRequired ? 'Yes' : 'No'}</td>
                        <td>
                            <button class="btn btn-sm btn-warning" onclick="edit(${x.officeTypeMasterId})">Edit</button>
                            <button class="btn btn-sm btn-danger" onclick="del(${x.officeTypeMasterId})">Delete</button>
                        </td>
                    </tr>`;
                });

    document.getElementById("tblBody").innerHTML = rows;
            });
    }

    function save() {
        let id = document.getElementById("Id").value;

    let obj = {
        officeTypeCode: document.getElementById("officeTypeCode").value,
    officeTypeName: document.getElementById("officeTypeName").value,
    menuFilePath: document.getElementById("MenuFilePath").value,
    bookingManifestRequired: document.getElementById("BookingManifestRequired").checked,
    prepaidBilling: document.getElementById("PrepaidBilling").checked,
    skipStockValidation: document.getElementById("SkipStockValidation").checked,
    deliveryChargeRequired: document.getElementById("DeliveryChargeRequired").checked,
        headOffice: document.getElementById("HeadOffice").checked
       

        };

        if (!obj.officeTypeCode || !obj.officeTypeName) {
        alert("Type Code and Type Name are required");
    return;
        }

    if (id) {
        fetch(`${apiUrl}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(obj)
        }).then(() => {
            loadData();
            clearForm();
        });
        } else {
        fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(obj)
        }).then(() => {
            loadData();
            clearForm();
        });
        }
    }

    function edit(id) {
        fetch(`${apiUrl}/${id}`)
            .then(res => res.json())
            .then(x => {
                debugger;
                document.getElementById("Id").value = x[0].officeTypeMasterId;
                document.getElementById("officeTypeCode").value = x[0].officeTypeCode;
                document.getElementById("officeTypeName").value = x[0].officeTypeName;
                document.getElementById("MenuFilePath").value = x[0].menuFilePath;

                document.getElementById("BookingManifestRequired").checked = x[0].bookingManifestRequired;
                document.getElementById("PrepaidBilling").checked = x[0].prepaidBilling;
                document.getElementById("SkipStockValidation").checked = x[0].skipStockValidation;
                document.getElementById("DeliveryChargeRequired").checked = x[0].deliveryChargeRequired;
                document.getElementById("HeadOffice").checked = x[0].headOffice;
            });
        // ✅ SHOW MODAL (correct way)
        let modal = new bootstrap.Modal(document.getElementById('officeTypeModal'));
        modal.show();
    }

    function del(id) {
        if (confirm("Are you sure to delete?")) {
        fetch(`${apiUrl}/${id}`, { method: "DELETE" })
            .then(() => loadData());
        }
    }

    function clearForm() {
        document.getElementById("Id").value = "";
        document.querySelectorAll("input").forEach(x => {
            if (x.type === "checkbox") x.checked = false;
    else x.value = "";
        });
    }

    loadData();