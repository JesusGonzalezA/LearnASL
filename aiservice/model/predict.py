# Imports --------------------------------------------------------------------------------------------------------------
import cv2
import os.path
import torch
import numpy as np
from model.utils import videotransforms, utils
from dotenv import load_dotenv
from torchvision import transforms
from model.pytorch_i3d import InceptionI3d

# Consts ---------------------------------------------------------------------------------------------------------------
load_dotenv()

NUM_CLASSES = int(os.getenv('NUM_CLASSES'))
FILE_MODEL = os.getenv('FILE_MODEL')
FILE_WEIGHTS = os.getenv('FILE_WEIGHTS')
DEVICE = os.getenv('DEVICE')
VIDEO_DIRECTORY = os.getenv('VIDEO_DIRECTORY')
VIDEO_NAME = os.getenv('VIDEO_NAME')
VIDEO_EXTENSION = os.getenv('VIDEO_EXTENSION')
NUM_CHANNELS = int(os.getenv('NUM_CHANNELS'))

def predict(filename):
    video = cv2.VideoCapture(filename)

    # Set up model  ----------------------------------------------------------------------------------------------------
    i3d = InceptionI3d(400, in_channels=NUM_CHANNELS)
    i3d.load_state_dict(torch.load(FILE_WEIGHTS, map_location=torch.device(DEVICE)))
    i3d.replace_logits(NUM_CLASSES)
    i3d.load_state_dict(torch.load(FILE_MODEL, map_location=torch.device(DEVICE)))
    i3d.eval()

    # Transform video --------------------------------------------------------------------------------------------------
    # Load video
    num_frames = int(video.get(cv2.CAP_PROP_FRAME_COUNT))

    # Transform video
    start_f = 0

    rgb_frames = utils.load_rgb_frames_from_video(video, start_f, num_frames)
    padded_frames = utils.pad(rgb_frames, num_frames)
    crop_transformations = transforms.Compose([videotransforms.CenterCrop(224)])
    transformed_video_as_images = crop_transformations(padded_frames)

    # Create tensor
    transformed_video_as_tensor = utils.video_to_tensor(transformed_video_as_images)
    input_model = torch.unsqueeze(transformed_video_as_tensor, 0)

    # Predict
    output = i3d(input_model)

    # Format result
    predictions = torch.max(output, dim=2)[0]
    predictions = predictions.cpu().detach().numpy()[0]
    out_index_labels = np.argsort(predictions)
    out_probs = np.sort(predictions)

    last_out_index_labels = np.take(out_index_labels, list(range(-10, 0)))
    last_out_probs = np.take(out_probs, list(range(-10, 0)))

    dictionary = utils.create_dictionary()
    last_out_labels = []
    for x in last_out_index_labels:
        last_out_labels.append(dictionary[x])

    return last_out_labels[::-1], last_out_probs[::-1]