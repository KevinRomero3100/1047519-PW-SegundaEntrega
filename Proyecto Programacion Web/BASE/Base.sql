-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema paycontroldb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema paycontroldb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `paycontroldb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `paycontroldb` ;

-- -----------------------------------------------------
-- Table `paycontroldb`.`cliente`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`cliente` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`cliente` (
  `IdCliente` INT NOT NULL AUTO_INCREMENT,
  `CodigoPersonal` INT NOT NULL,
  `DPI` INT NOT NULL,
  `PrimerNombre` VARCHAR(20) NOT NULL,
  `SegundoNombre` VARCHAR(20) NULL DEFAULT NULL,
  `PrimerApellido` VARCHAR(20) NOT NULL,
  `SegundoApellido` VARCHAR(20) NULL DEFAULT NULL,
  `Telefono` INT NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `Estado` TINYINT NOT NULL,
  PRIMARY KEY (`IdCliente`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`colonia`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`colonia` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`colonia` (
  `idColonia` INT NOT NULL,
  `Nombre` VARCHAR(30) NULL DEFAULT NULL,
  `Municipio` VARCHAR(30) NULL DEFAULT NULL,
  `Departamento` VARCHAR(30) NULL DEFAULT NULL,
  PRIMARY KEY (`idColonia`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`direccion`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`direccion` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`direccion` (
  `idDireccion` INT NOT NULL,
  `Referencia` VARCHAR(75) NOT NULL,
  `Descripcion` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idDireccion`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`cliente-direeccion`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`cliente-direeccion` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`cliente-direeccion` (
  `idCliente-Direeccion` INT NOT NULL,
  `colonia_idColonia` INT NOT NULL,
  `direccion_idDireccion` INT NOT NULL,
  `cliente_IdCliente` INT NOT NULL,
  PRIMARY KEY (`idCliente-Direeccion`),
  INDEX `fk_cliente-direeccion_colonia1_idx` (`colonia_idColonia` ASC) VISIBLE,
  INDEX `fk_cliente-direeccion_direccion1_idx` (`direccion_idDireccion` ASC) VISIBLE,
  INDEX `fk_cliente-direeccion_cliente1_idx` (`cliente_IdCliente` ASC) VISIBLE,
  CONSTRAINT `fk_cliente-direeccion_cliente1`
    FOREIGN KEY (`cliente_IdCliente`)
    REFERENCES `paycontroldb`.`cliente` (`IdCliente`),
  CONSTRAINT `fk_cliente-direeccion_colonia1`
    FOREIGN KEY (`colonia_idColonia`)
    REFERENCES `paycontroldb`.`colonia` (`idColonia`),
  CONSTRAINT `fk_cliente-direeccion_direccion1`
    FOREIGN KEY (`direccion_idDireccion`)
    REFERENCES `paycontroldb`.`direccion` (`idDireccion`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`factura`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`factura` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`factura` (
  `idFactura` INT NOT NULL,
  `FechaDeEmision` DATE NOT NULL,
  `FechaDeIngreso` DATETIME NOT NULL,
  `NumeroDeFactura` INT NOT NULL,
  `ImporteTotal` DOUBLE NOT NULL,
  `IVA` DOUBLE NOT NULL,
  `cliente_IdCliente` INT NOT NULL,
  PRIMARY KEY (`idFactura`),
  INDEX `fk_factura_cliente1_idx` (`cliente_IdCliente` ASC) VISIBLE,
  CONSTRAINT `fk_factura_cliente1`
    FOREIGN KEY (`cliente_IdCliente`)
    REFERENCES `paycontroldb`.`cliente` (`IdCliente`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`menusualidad`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`menusualidad` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`menusualidad` (
  `idMenusualidad` INT NOT NULL,
  `Mes` INT NOT NULL,
  `Año` INT NOT NULL,
  PRIMARY KEY (`idMenusualidad`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`detallefactura`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`detallefactura` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`detallefactura` (
  `idDetalle` INT NOT NULL,
  `TipodeServicio` TINYINT NOT NULL,
  `factura_idFactura` INT NOT NULL,
  `menusualidad_idMenusualidad` INT NOT NULL,
  PRIMARY KEY (`idDetalle`),
  INDEX `fk_detallefactura_factura1_idx` (`factura_idFactura` ASC) VISIBLE,
  INDEX `fk_detallefactura_menusualidad1_idx` (`menusualidad_idMenusualidad` ASC) VISIBLE,
  CONSTRAINT `fk_detallefactura_factura1`
    FOREIGN KEY (`factura_idFactura`)
    REFERENCES `paycontroldb`.`factura` (`idFactura`),
  CONSTRAINT `fk_detallefactura_menusualidad1`
    FOREIGN KEY (`menusualidad_idMenusualidad`)
    REFERENCES `paycontroldb`.`menusualidad` (`idMenusualidad`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`rol`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`rol` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`rol` (
  `idRol` INT NOT NULL AUTO_INCREMENT,
  `Type` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idRol`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`empleado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`empleado` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`empleado` (
  `idEmpleado` INT NOT NULL AUTO_INCREMENT,
  `CodigoPersonal` INT NOT NULL,
  `DPI` INT NOT NULL,
  `PrimerNombre` VARCHAR(20) NOT NULL,
  `SegundoNombre` VARCHAR(20) NULL DEFAULT NULL,
  `PrimerApellido` VARCHAR(20) NOT NULL,
  `SegundoApellido` VARCHAR(20) NULL DEFAULT NULL,
  `Telefono` INT NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `Estado` TINYINT NOT NULL,
  `rol_idRol` INT NOT NULL,
  PRIMARY KEY (`idEmpleado`),
  INDEX `fk_empleado_rol1_idx` (`rol_idRol` ASC) VISIBLE,
  CONSTRAINT `fk_empleado_rol1`
    FOREIGN KEY (`rol_idRol`)
    REFERENCES `paycontroldb`.`rol` (`idRol`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_vietnamese_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`direccion-empleado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`direccion-empleado` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`direccion-empleado` (
  `idDireccion-Empleado` INT NOT NULL,
  `empleado_idEmpleado` INT NOT NULL,
  `direccion_idDireccion` INT NOT NULL,
  PRIMARY KEY (`idDireccion-Empleado`),
  INDEX `fk_direccion-empleado_direccion1_idx` (`direccion_idDireccion` ASC) VISIBLE,
  INDEX `fk_direccion-empleado_empleado1_idx` (`empleado_idEmpleado` ASC) VISIBLE,
  CONSTRAINT `fk_direccion-empleado_direccion1`
    FOREIGN KEY (`direccion_idDireccion`)
    REFERENCES `paycontroldb`.`direccion` (`idDireccion`),
  CONSTRAINT `fk_direccion-empleado_empleado1`
    FOREIGN KEY (`empleado_idEmpleado`)
    REFERENCES `paycontroldb`.`empleado` (`idEmpleado`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`facturaempleado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`facturaempleado` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`facturaempleado` (
  `idFacturaEmpleado` INT NOT NULL,
  `factura_idFactura` INT NOT NULL,
  `empleado_idEmpleado` INT NOT NULL,
  PRIMARY KEY (`idFacturaEmpleado`),
  INDEX `fk_facturaempleado_factura1_idx` (`factura_idFactura` ASC) VISIBLE,
  INDEX `fk_facturaempleado_empleado1_idx` (`empleado_idEmpleado` ASC) VISIBLE,
  CONSTRAINT `fk_facturaempleado_empleado1`
    FOREIGN KEY (`empleado_idEmpleado`)
    REFERENCES `paycontroldb`.`empleado` (`idEmpleado`),
  CONSTRAINT `fk_facturaempleado_factura1`
    FOREIGN KEY (`factura_idFactura`)
    REFERENCES `paycontroldb`.`factura` (`idFactura`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `paycontroldb`.`usuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `paycontroldb`.`usuario` ;

CREATE TABLE IF NOT EXISTS `paycontroldb`.`usuario` (
  `idUsuario` INT NOT NULL,
  `NombreDeUsuario` VARCHAR(45) NOT NULL,
  `Pasword` VARCHAR(45) NOT NULL,
  `empleado_idEmpleado` INT NOT NULL,
  PRIMARY KEY (`idUsuario`, `empleado_idEmpleado`),
  INDEX `fk_usuario_empleado1_idx` (`empleado_idEmpleado` ASC) VISIBLE,
  CONSTRAINT `fk_usuario_empleado1`
    FOREIGN KEY (`empleado_idEmpleado`)
    REFERENCES `paycontroldb`.`empleado` (`idEmpleado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
