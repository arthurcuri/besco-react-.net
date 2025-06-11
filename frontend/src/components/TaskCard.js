import React from 'react';
import { useSortable } from '@dnd-kit/sortable';
import { CSS } from '@dnd-kit/utilities';
import { DeleteIcon, DragIcon } from './Icons';
import './TaskCard.css';

const TaskCard = ({ task, onDelete }) => {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({
    id: task.id,
  });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    opacity: isDragging ? 0.5 : 1,
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('pt-BR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  const getStatusInfo = (status) => {
    const statusMap = {
      'Pending': { 
        text: 'Pendente', 
        badgeClass: 'status-pending',
        cardClass: 'task-card-pending'
      },
      'InProgress': { 
        text: 'Em Andamento', 
        badgeClass: 'status-progress',
        cardClass: 'task-card-progress'
      },
      'Completed': { 
        text: 'Concluída', 
        badgeClass: 'status-completed',
        cardClass: 'task-card-completed'
      }
    };
    return statusMap[status] || { 
      text: 'Unknown', 
      badgeClass: 'status-unknown',
      cardClass: 'task-card-unknown'
    };
  };

  const statusInfo = getStatusInfo(task.status);

  return (
    <div
      ref={setNodeRef}
      style={style}
      className={`task-card ${statusInfo.cardClass} ${isDragging ? 'dragging' : ''}`}
    >
      <div className="task-header">
        <div 
          {...attributes}
          {...listeners}
          className="task-drag-area"
        >
          <h4 className="task-title">{task.title}</h4>
        </div>
        <button
          className="delete-btn"
          onClick={(e) => {
            e.preventDefault();
            e.stopPropagation();
            console.log('Clicou no delete da tarefa:', task.id, task.title);
            if (window.confirm(`Excluir "${task.title}"?`)) {
              onDelete(task.id);
            }
          }}
          title="Excluir tarefa"
          type="button"
        >
          <DeleteIcon size={16} color="#ff6b6b" />
        </button>
      </div>
      
      <div 
        {...attributes}
        {...listeners}
        className="task-content"
      >
        {task.description && (
          <p className="task-description">{task.description}</p>
        )}
        
        <div className="task-footer">
          <span className={`status-badge ${statusInfo.badgeClass}`}>
            {statusInfo.text}
          </span>
          <small className="task-date">
            {formatDate(task.createdAt)}
          </small>
        </div>
      </div>
      
      <div className="drag-handle">
        <DragIcon size={16} color="#606060" />
      </div>
    </div>
  );
};

export default TaskCard; 