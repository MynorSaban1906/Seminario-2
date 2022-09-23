use semi2_base2;

SELECT * FROM  Temporalvent_base1 ;
SELECT * FROM Temporalcomp_base1 t;


CREATE TABLE Temporalcomp_base1(
	Fecha varchar(10),
	Codproveedor varchar(100),
	Nombreproveedor varchar(100),
	Direccionproveedor varchar(100),
	Numeroproveedor varchar(100),
	Webproveedor varchar(100),
    
	Codproducto varchar(100),
	Nombreproducto varchar(100),
	Marcaproducto varchar(100),
	Categoria varchar(100),
    
    
	Codsucursal varchar(100),
	Nombresucursal varchar(100),
	Direccionsucursal varchar(100),
	Region varchar(100),
	Departamento varchar(100),
    
	Unidades varchar(100),
	Costou varchar(100)
);



CREATE TABLE Temporalvent_base1(
	Fecha varchar(100),
	Codigocliente varchar(100),
	Nombrecliente varchar(100),
	Tipocliente varchar(100),
	Direccioncliente varchar(100),
	Numerocliente varchar(100),
	Codvendedor varchar(100),
	Nombrevendedor varchar(100),
	Vacacionista varchar(100),
	Codproducto varchar(100),
	Nombreproducto varchar(100),
	Marcaproducto varchar(100),
	Categoria varchar(100),
	Codsucursal varchar(100),
	Nombresucursal varchar(100),
	Direccionsucursal varchar(100),
	Region varchar(100),
	Departamento varchar(100),
	Unidades varchar(100),
	Preciounitario varchar(100)					
);

