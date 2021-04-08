/// <reference path="DescVal.aspx" />
/// <reference path="DescVal.aspx" />


function fill() {
    $('#Seg').append('<option value=-1>Seleccione</option><option value=12>Nacional</option><option value=12>Grande</option><option value=10>Mediano</option><option value=7>Pequeño</option><option value=1>AutoConstructor</option>');
    $('#Logis').append('<option value=-1>Seleccione</option><option value=2>Distancia Baja(2km)</option><option value=0>Distancia Media</option>');
    $('#SegCli').append('<option value=-1>Seleccione</option><option value=9000052007>Industriales</option><option value=9000052008>Constructores y Desarrolladores</option><option value=9000052009>Gobierno</option><option value=9000052010>Distribuidores</option><option value=9000052046>Otras Transacciones</option>');
    $('#SegReg').append('<option value=-1>Seleccione</option><option value=9000049197>ANTIOQUIA</option><option value=9000030617>BARRANQUILLA Y AM</option>'+
        '<option value=9000049187>BOGOTÁ</option><option value=9000046545>BUCARAMANGA Y AM</option>'+
         '<option value=9000046482>CARTAGENA Y AM</option><option value=9000046463>CUCUTA</option>'+
         '<option value=9000049203>CUNDINAMARCA</option><option value=9000049203>ANAPOIMA</option><option value=9000046548>IBAGUE</option>' +
         '<option value=9000030636>NEIVA</option><option value=9000046543>PEREIRA</option>'+
          '<option value=9000046495>S. MARTA / CIENAGA</option><option value=9000031876>TULUA (VAL)</option>'+
          '<option value=9000049213>VALLE DEL CAUCA</option><option value=9000046524>VALLEDUPAR</option>'+
          '<option value=9000046479>VILLAVICENCIO</option>');
    $('#planta').append('<option value=-1>Seleccione</option><option value=1>PLANTA ANAPOIMA</option>' +
                 '<option value=2>PLANTA RONEGRO</option>' + '<option value=0>NO APLICA PLANTA</option>');

    $('#TObra').append('<option value=-1>Seleccione</option><option value=8>Vivienda de Interés</option><option value=0>No VI</option>');
           


    $('#Validar').click(function () {
      
        $('#Desc').text('');
        $('#Desc').show();
        if (parseInt($('#Seg').val()) == -1 || parseInt($('#Logis').val()) == -1 || parseInt($('#planta').val()) == -1 || parseInt($('#SegCli').val()) == -1 || parseInt($('#SegReg').val()) == -1 || parseInt($('#TObra').val()) == -1) {
            alert("Por favor, seleccione un valor para todos los campos obligatorios (*)")
        }
        else {
            
            $.ajax({
                url: 'Handler_Descuento/Descuentos.ashx',
                data: { 'Segcli': $('#SegCli').val(), 'SegReg': $('#SegReg').val(), 'planta': $('#planta').val() },
                dataType: 'text', 
                type: 'POST',
                error: function () {
                  
                    $('#Desc').text("Error: Por favor verifique la relación segmentación cliente región y planta");
                    $('#ctl00_MainContent_Desc').text("Error: Por favor verifique la relación segmentación cliente región y planta");
                },
                
                success: function (respuestaTitulo) {
                    
                    var str = respuestaTitulo;
                    var res = parseFloat(str.replace(/['"]+/g, ''));
                    var logistica = parseInt($('#Seg').val());
                    var obra = parseInt($('#TObra').val());
                    var segmento = parseInt($('#Logis').val())
                    var resultado = parseFloat(segmento + logistica + obra + res);


                    $('#Desc').text("El descuento máximo de la Obra es: " + resultado + "%");
                    $('#ctl00_MainContent_Desc').text("El descuento máximo de la Obra es: " + resultado + "%");

                }

                });
               
        }
    });
};