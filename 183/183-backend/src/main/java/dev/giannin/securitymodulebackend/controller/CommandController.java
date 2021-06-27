package dev.giannin.securitymodulebackend.controller;

import dev.giannin.securitymodulebackend.model.command.CommandException;
import dev.giannin.securitymodulebackend.model.command.CommandResultDto;
import dev.giannin.securitymodulebackend.service.CommandService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/command")
public class CommandController {
    private CommandService commandService;

    @Autowired
    public CommandController(CommandService commandService) {
        this.commandService = commandService;
    }

    @ResponseStatus(HttpStatus.OK)
    @GetMapping(produces = "application/json")
    public CommandResultDto executeCommand(@RequestParam(required = false) String[] options) throws CommandException {
        return commandService.executeCommand(options);
    }
}
