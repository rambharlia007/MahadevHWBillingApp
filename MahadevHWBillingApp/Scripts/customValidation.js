var validation = $(function () {

    function removeWarningMessage(e) {
        setTimeout(function () {
            e.next("span").remove();
        }, 2000);
    }

    return {
        getGSTStateCode: function () {
            var stateCode = [
                { Name: "Select state", Code: -1 },
                { Name: "JAMMU AND KASHMIR", Code: 1 },
                { Name: "HIMACHAL PRADESH", Code: 2 },
                { Name: "PUNJAB", Code: 3 },
                { Name: "CHANDIGARH", Code: 4 },
                { Name: "UTTARAKHAND", Code: 5 },
                { Name: "HARYANA", Code: 6 },
                { Name: "DELHI", Code: 7 },
                { Name: "RAJASTHAN", Code: 8 },
                { Name: "UTTAR PRADESH", Code: 9 },
                { Name: "BIHAR", Code: 10 },
                { Name: "SIKKIM", Code: 11 },
                { Name: "ARUNACHAL PRADESH", Code: 12 },
                { Name: "NAGALAND", Code: 13 },
                { Name: "MANIPUR", Code: 14 },
                { Name: "MIZORAM", Code: 15 },
                { Name: "TRIPURA", Code: 16 },
                { Name: "MEGHLAYA", Code: 17 },
                { Name: "ASSAM", Code: 18 },
                { Name: "WEST BENGAL", Code: 19 },
                { Name: "JHARKHAND", Code: 20 },
                { Name: "ODISHA", Code: 21 },
                { Name: "CHATTISGARH", Code: 22 },
                { Name: "MADHYA PRADESH", Code: 23 },
                { Name: "GUJARAT", Code: 24 },
                { Name: "DAMAN AND DIU", Code: 25 },
                { Name: "DADRA AND NAGAR HAVELI", Code: 26 },
                { Name: "MAHARASHTRA", Code: 27 },
                { Name: "ANDHRA PRADESH(BEFORE DIVISION)", Code: 28 },
                { Name: "KARNATAKA", Code: 29 },
                { Name: "GOA", Code: 30 },
                { Name: "LAKSHWADEEP", Code: 31 },
                { Name: "KERALA", Code: 32 },
                { Name: "TAMIL NADU", Code: 33 },
                { Name: "PUDUCHERRY", Code: 34 },
                { Name: "ANDAMAN AND NICOBAR ISLANDS", Code: 35 },
                { Name: "TELANGANA", Code: 36 },
                { Name: "ANDHRA PRADESH(NEW)", Code: 37 }
            ];
            return stateCode;
        },
        isFormDataValid: function () {
            var x;
            $("form").each(function () {
                x = $(this).find(':input.form-control');
            });
            for (i = 0; i < x.length; i++) {
                var ele = $(x[i]);
                ele.focusout();
            }
            return $("form .custom-alert").length;
        },
        confirm: function (mes) {
            $("#confirmation-modal").modal("show").on("click", ".confirm-success", function () {
                $("#confirmation-modal").modal("hide");
                return true;
            }).on("click", ".confirm-failure", function () {
                $("#confirmation-modal").modal("hide");
                return false;
            });
                
            //$("#confirmation-modal .modal-body p").text(mes);

            //$(".confirm-success").on("click", function () {
            //    $("#confirmation-modal").modal("hide");
            //    return true;
            //});

            //$(".confirm-failure").on("click", function () {
            //    $("#confirmation-modal").modal("hide");
            //    return false;
            //});

        },
        isTableDataValid: function (id) {
            var x = $("#" + id).find(":input.form-control");
            for (i = 0; i < x.length; i++) {
                var ele = $(x[i]);
                ele.focusout();
            }
            return $("table .custom-alert").length;
        },
        removeErrorMessage: function () {
            $(".custom-alert").remove();
        },
        alertCollapseCardError: function (id) {
            var ele = $(id + " .card");
            for (i = 0; i < ele.length; i++) {
                if ($(ele[i]).find(".collapse").length > 0 && $(ele[i]).find(".custom-alert").length > 0) {
                    $(ele[i]).addClass("card-alert");
                    $(ele[i]).find(".card-header").addClass("card-alert-header");
                }
                else {
                    $(ele[i]).removeClass("card-alert");
                    $(ele[i]).find(".card-header").removeClass("card-alert-header");
                }
            }
        },
        collapse: function () {
            var ele = $(".table-body .card-body");
            for (i = 0; i < ele.length - 1; i++) {
                $(ele[i]).addClass("collapse");
            }
        },
        removeCardCollapse: function () {
            $(".card .card-body").removeClass("collapse");
        },
        cardToggle: function (index) {
            var ele = $(".table-body .card");
            var collEle = $(ele[index]);
            var cardBodyEle = collEle.find(".card-body");
            if (collEle.find(".collapse").length === 0) {
                cardBodyEle.addClass("collapse");
                if (collEle.find(".custom-alert").length > 0) {
                    collEle.addClass("card-alert");
                    collEle.find(".card-header").addClass("card-alert-header");
                }
            } else {
                collEle.removeClass("card-alert");
                collEle.find(".card-header").removeClass("card-alert-header");
                cardBodyEle.removeClass("collapse");
            }
        },
        register: function () {

           
            //is only for date picker
            //$(':input[cv="date"]').focusout(function (e) {
            //    var currEle = $(e.target).parent();
            //    var inpEle = currEle.find(":input");
            //    currEle.next("span .custom-alert").remove();
            //    for (i = 1; i < inpEle.length; i++) {
            //        var value = $(inpEle).val();
            //        var isNotValid = isNaN(parseInt(value));
            //        if (isNotValid) {
            //            currEle.next("span").remove();
            //            currEle.after("<span class='custom-alert'>Field is required</span>");
            //        }
            //        else {
            //            currEle.next("span").remove();
            //        }
            //    }
            //});

            setTimeout(function () {
                $('[data-toggle="tooltip"]').tooltip();
            }, 1000);

            $(':input[type="number"]').keypress(function (e) {
                var inputKeyCode = e.keyCode ? e.keyCode : e.which;
                if (inputKeyCode !== null) {
                    if (inputKeyCode === 45) {
                        $(e.target).next("span").remove();
                        $(e.target).after("<span class='custom-alert'>negative numbers are not allowed</span>");
                        e.preventDefault();
                        removeWarningMessage($(e.target));
                    }
                }
            });

            $(':input[cv="phone"]').focusout(function (e) {
                $(e.target).removeClass("correct");
                var value = $(e.target).val();
                var count = value.length;
                //if (!count) {
                //    $(e.target).next("span").remove();
                //    return;
                //}

                if (value.includes("+") && count !== 14 && value.includes(" ")) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Phone number is not valid.</span>");
                }
                else if (value.includes("+") && count !== 13 && !value.includes(" ")) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Phone number is not valid.</span>");
                }
                else if (!value.includes("+") && count !== 10) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Phone number is not valid.</span>");
                }
                else if (value.length > 0) {
                    $(e.target).addClass("correct");
                    $(e.target).next("span").remove();
                }

                if (value.length < 1) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Field is required.</span>");
                }
            });

            $(':input[cv="phone"]').keypress(function (e) {
                var validKeys = ["+", " "];
                var key = e.key.toUpperCase();
                if (key !== null && (isNaN(parseInt(key)) && !validKeys.includes(key))) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Phone number is not valid.</span>");
                    e.preventDefault();
                    removeWarningMessage($(e.target));
                }
            });

            $(':input[cv="gstin"]').focusout(function (e) {
                $(e.target).removeClass("correct");
                var gstinFormat = new RegExp('^([0]{1}[1-9]{1}|[1-2]{1}[0-9]{1}|[3]{1}[0-7]{1})([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$');
                var value = $(e.target).val();
                if ((!gstinFormat.test(value) && value.length > 0)) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>GSTIN number is not valid.</span>");
                }
                else if (value.length < 1) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Field is required.</span>");
                }
                else if (value.length > 0) {
                    $(e.target).addClass("correct");
                    $(e.target).next("span").remove();
                }
            });

            $(':input[cv="required"]').focusout(function (e) {
                $(e.target).removeClass("correct");
                var compare = RegExp("\\d{1,}");
                var value = $(e.target).val();
                if (value.length < 1) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Field is required.</span>");
                } else if (value.length > 0) {
                    $(e.target).addClass("correct");
                    $(e.target).next("span").remove();
                }
            });

            $(':input[type="email"]').focusout(function (e) {
                $(e.target).removeClass("correct");
                var email = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                var value = $(e.target).val();
                if ((!email.test(value) && value.length > 0)) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Email Id is not valid.</span>");
                }
                else if (value.length < 1) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Field is required.</span>");
                }
                else if (value.length > 0) {
                    $(e.target).addClass("correct");
                    $(e.target).next("span").remove();
                }
            });

            $(':input[cv="username"]').focusout(function (e) {
                $(e.target).removeClass("correct");
                var userName = /^[a-z0-9_-]{3,16}$/;
                var value = $(e.target).val();
                if ((!userName.test(value) && value.length > 0)) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>user name is not valid.</span>");
                }
                else if (value.length < 1) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Field is required.</span>");
                }
                else if (value.length > 0) {
                    $(e.target).addClass("correct");
                    $(e.target).next("span").remove();
                }
            });

            $(':input[cv="confirmpassword"]').focusout(function (e) {
                $(e.target).removeClass("correct");
                $("#password").removeClass("correct");
                var password = $("#password").val();
                var confirmPassword = $(e.target).val();
                if (password !== confirmPassword) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>password and confirm password not matching.</span>");
                }
                else if (password.length < 1) {
                    $(e.target).next("span").remove();
                    $(e.target).after("<span class='custom-alert'>Field is empty.</span>");
                    $("#password").next("span").remove();
                    $("#password").after("<span class='custom-alert'>Field is empty.</span>");
                }
                else {
                    $("#password").addClass("correct");
                    $("#password").next("span").remove();
                    $(e.target).addClass("correct");
                    $(e.target).next("span").remove();
                }
            });
        }
    };
}());

